using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Camew;
using Camew.Lottery;
using Jueci.MobileWeb.Repositories;
using Camew.Lottery.AppService;
using System.Timers;
using System.Xml.Linq;
using Abp.Logging;
using Camew.Extend;
using Jueci.MobileWeb.Lottery.Event;
using Newtonsoft.Json.Linq;

namespace Jueci.MobileWeb.Lottery.Service
{
    public class LotteryServiceManager : ILotteryServiceManager
    {

        public static string Constr { get; private set; }
        /// <summary>
        /// 开奖数据数据库连接字符串
        /// </summary>
        public static string CPConstr { get; private set; }
        //public LotteryEngine LotteryEngine { get; private set; }

        private static Dictionary<CPType, LotteryServiceInfo> _LotteryEngineManagers = null;

        private static List<CPType> cpTypeList = null;

        private readonly ILotteryConfigRepository _lotteryConfigRepository;
        private readonly ICPDataRepository _cpDataRepository;
       // private readonly object lockObj = new object();


        private Timer _moniteTimer;

        /// <summary>
        /// 获取指定服务的管理器
        /// </summary>
        /// <param name="sid">服务标识</param>
        /// <returns></returns>
        public LotteryServiceInfo GetServiceManager(CPType cpType)
        {
            return _LotteryEngineManagers.GetValue(cpType);
        }

        static LotteryServiceManager()
        {
            Constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            CPConstr = ConfigurationManager.ConnectionStrings["cpconstr"].ConnectionString;

            cpTypeList = ConfigurationManager.AppSettings["CpTypeList"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(
                x => (CPType)Enum.Parse(typeof(CPType), x)).ToList();
            _LotteryEngineManagers = new Dictionary<CPType, LotteryServiceInfo>(cpTypeList.Count);
           
        }

    

        public LotteryServiceManager(ILotteryConfigRepository lotteryConfigRepository,
            ICPDataRepository cpDataRepository)
        {
            _lotteryConfigRepository = lotteryConfigRepository;
            _cpDataRepository = cpDataRepository;
            InitLotteryManager(cpTypeList);
        }

        private  void InitLotteryManager(List<CPType> cpTypes)
        {
            foreach (var cpType in cpTypes)
            {
                if (!_LotteryEngineManagers.ContainsKey(cpType))
                {

                    var result = _lotteryConfigRepository.GetServiceInitConfig(cpType);
                    if (result.Result != 0)
                    {
                        throw new Exception("载入" + ServiceInitConfigName.SERVICECONFIG + "时出错：" + result.Remarks);
                    }
                    XElement config = result.Data;

                    //服务名称
                    //XAttribute xa = config.Attribute("Name");
                    //ServiceName = xa.Value;

                    //初始化彩票引擎
                    var lotteryLotteryEngine = LotteryEngine.CreateLotteryEngine(config);

                    var lotteryServiceInfo = new LotteryServiceInfo(lotteryLotteryEngine);
                    lotteryServiceInfo.UpdateLotteryData.UpdateLotteryEventHandler += UpdateLotteryDataEventHandler;
                     _LotteryEngineManagers.Add(cpType, lotteryServiceInfo);

                    LogHelper.Logger.Info("服务 " + GetServiceName(cpType) + " 初始化成功。");
                   
                }
            }
        }

        private void UpdateLotteryDataEventHandler(object sender, Event.EventData.UpdateLotteyDataEventArgs e)
        {
            lock (_LotteryEngineManagers)
            {
                var lotteryEngine = e.LotteryEngine;
                LogHelper.Logger.Info(string.Format("定时执行更新彩票数据，彩票类型：{0}", e.LotteryEngine.Lottery.Name));
                if (!lotteryEngine.CheckNeedUpdateData()) return;
                //从引擎的附加属性中获取强制更新全部开奖数据的标识
                object latestDataFlag = lotteryEngine.AttachProperties[EngineAttachPropertyKey.LATESTDATAFLAG];
                if (latestDataFlag == null)
                    latestDataFlag = 0;
                // ResultObject ret = DBActions.DBAction.GetLatestCPData(this.LotteryEngine.Lottery.ID, this.LotteryEngine.GetLatestCPDataInfo().Data.ID, (int)latestDataFlag, this.LotteryEngine.Lottery.MaxDataCount);
                ResultObject ret = _cpDataRepository.GetLatestCPData(lotteryEngine.Lottery.ID, lotteryEngine.GetLatestCPDataInfo().Data.ID, (int)latestDataFlag, lotteryEngine.Lottery.MaxDataCount);
                if (ret.Result == 0)
                {
                    List<CPData> data = ret.Data as List<CPData>;
                    if (data != null && data.Count > 0)
                    {

                        //在引擎的附加属性中设置强制更新全部开奖数据的标识
                        int updateFlag = Convert.ToInt32(ret.Remarks);
                        lotteryEngine.AttachProperties[EngineAttachPropertyKey.LATESTDATAFLAG] = updateFlag;

                        //将数据更新到数据引擎中
                        List<CPData> newDatas = lotteryEngine.UpdateLotteryData(data, updateFlag > (int)latestDataFlag);

                        //在引擎的附加属性中缓存开奖数据
                        List<JsonData> jsonList = GetJsonCpDataList(newDatas.OrderByDescending(x => x.ID).ToList());
                        lotteryEngine.AttachProperties[EngineAttachPropertyKey.CPDATA] = jsonList;


                        //if (IsDebug)
                        //{
                        //	//最新一期的开奖数据信息以及下一期开奖期数和时间
                        //    CPDataExtend latestCPDataInfo = LotteryEngine.GetLatestCPDataInfo();
                        //    DevTools.WriteNote("成功更新开奖数据：" + latestCPDataInfo.Data.ToString() + ",下一期开奖时间：" + latestCPDataInfo.NextCPDataOpenTime.ToString());
                        //}

                    }
                }
            }
          
        }

        private string GetServiceName(CPType cpType)
        {
            string serviceName = String.Empty;
            switch (cpType)
            {
                case CPType.cqssc:
                    serviceName = "重启时时彩";
                    break;
                case CPType.pks:
                    serviceName = "PK拾";
                    break;
                default:
                    serviceName = "其他服务";
                    break;
            }
            return serviceName;
        }




        //private void UpdateLotteryData(LotteryEngine lotteryEngine)
        //{
        //    if (lotteryEngine.CheckNeedUpdateData())
        //    {
        //        //从引擎的附加属性中获取强制更新全部开奖数据的标识
        //        object latestDataFlag = lotteryEngine.AttachProperties[EngineAttachPropertyKey.LATESTDATAFLAG];
        //        if (latestDataFlag == null)
        //            latestDataFlag = 0;
        //        // ResultObject ret = DBActions.DBAction.GetLatestCPData(this.LotteryEngine.Lottery.ID, this.LotteryEngine.GetLatestCPDataInfo().Data.ID, (int)latestDataFlag, this.LotteryEngine.Lottery.MaxDataCount);
        //        ResultObject ret = _cpDataRepository.GetLatestCPData(lotteryEngine.Lottery.ID, lotteryEngine.GetLatestCPDataInfo().Data.ID, (int)latestDataFlag, lotteryEngine.Lottery.MaxDataCount);
        //        if (ret.Result == 0)
        //        {
        //            List<CPData> data = ret.Data as List<CPData>;
        //            if (data != null && data.Count > 0)
        //            {

        //                //在引擎的附加属性中设置强制更新全部开奖数据的标识
        //                int updateFlag = Convert.ToInt32(ret.Remarks);
        //                lotteryEngine.AttachProperties[EngineAttachPropertyKey.LATESTDATAFLAG] = updateFlag;

        //                //将数据更新到数据引擎中
        //                List<CPData> newDatas = lotteryEngine.UpdateLotteryData(data, updateFlag > (int)latestDataFlag);

        //                //在引擎的附加属性中缓存开奖数据
        //                List<JsonData> jsonList = GetJsonCpDataList(newDatas.OrderByDescending(x => x.ID).ToList());
        //                lotteryEngine.AttachProperties[EngineAttachPropertyKey.CPDATA] = jsonList;


        //                //if (IsDebug)
        //                //{
        //                //	//最新一期的开奖数据信息以及下一期开奖期数和时间
        //                //    CPDataExtend latestCPDataInfo = LotteryEngine.GetLatestCPDataInfo();
        //                //    DevTools.WriteNote("成功更新开奖数据：" + latestCPDataInfo.Data.ToString() + ",下一期开奖时间：" + latestCPDataInfo.NextCPDataOpenTime.ToString());
        //                //}

        //            }
        //        }
        //    }
        //}

        private List<JsonData> GetJsonCpDataList(List<CPData> cpDataList, int pageSize = 20)
        {
            List<JsonData> jsonDataList = new List<JsonData>((cpDataList.Count / pageSize) + 1);
            JArray pageJson = null;
            JObject dataJson = null;
            for (int pageIndex = 0; pageIndex < cpDataList.Count; pageIndex += pageSize)
            {
                int actualPageEndIndex = pageIndex + pageSize - 1;
                if (actualPageEndIndex >= cpDataList.Count)
                    actualPageEndIndex = cpDataList.Count - 1;
                pageJson = new JArray();
                for (int itemIndex = pageIndex; itemIndex <= actualPageEndIndex; itemIndex++)
                {
                    dataJson = new JObject();
                    CPData cpData = cpDataList[itemIndex];
                    dataJson["ID"] = cpData.ID;
                    dataJson["Data"] = cpData.Data;
                    dataJson["CreateTime"] = cpData.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    pageJson.Add(dataJson);
                }
                jsonDataList.Add(new JsonData(pageJson));
            }
            return jsonDataList;
        }
    }
}