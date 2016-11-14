using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Abp.Domain.Repositories;
using Abp.Logging;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Common.Caches.Impl;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Repositories;

namespace Jueci.MobileWeb.Lottery.Service.LotteryPlanManager
{
    public class LotteryPlanManager : ILotteryPlanManager
    {
        private CacheDictionary<string, PlanComputionData> _planComputionCache;

        private readonly IRepository<LotteryPlanLib,string> _lotteryPlanLibRepository;
     
        public LotteryPlanManager(IRepository<LotteryPlanLib,string> lotteryPlanLibRepository)
        {
            _lotteryPlanLibRepository = lotteryPlanLibRepository;

            _planComputionCache = new CacheDictionary<string, PlanComputionData>();
        }


        public bool UpdateUserLotteryPlan(string id,List<PlanComputionInfo> planInfos,LotteryPlanLib lotteryPlanLib)
        {
            lock (_planComputionCache)
            {
                if (!_planComputionCache.ContainsKey(id))
                {
                    _planComputionCache.Add(id, new PlanComputionData(planInfos, lotteryPlanLib));
                    _planComputionCache[id].OperateTime = DateTime.Now;
                }
                else
                {
                    _planComputionCache[id] = new PlanComputionData(planInfos, lotteryPlanLib);
                    _planComputionCache[id].OperateTime = DateTime.Now;
                }
                return true;
            }
          
        }

        public List<PlanComputionInfo> GetComputionInfos(string id, LotteryEngine sscLotteryEngine/*, ref bool isNeedUpdateCache*/)
        {
            lock (_planComputionCache)
            {
                if (_planComputionCache.ContainsKey(id))
                {
                    _planComputionCache[id].OperateTime = DateTime.Now;
                   // isNeedUpdateCache = false;
                    return _planComputionCache[id].PlanComputionList;
                }
                var planLibInfo = _lotteryPlanLibRepository.Single(p => p.Id == id);
                if (planLibInfo == null)
                {
                    string msg = string.Format("不存在Id为{0}计划，请检查您输入的url是否正确", id);
                    LogHelper.Logger.Error(msg);
                    throw new Exception(msg);
                }
               // isNeedUpdateCache = true;
                var plancomputeInfos = sscLotteryEngine.ConvertPCListFromXml(XElement.Parse(planLibInfo.PlanComputionInfo));
                this.UpdateUserLotteryPlan(id, plancomputeInfos, planLibInfo);
                return plancomputeInfos;
            }
        }

        public PlanComputionData GetComputionData(string id, LotteryEngine sscLotteryEngine)
        {
            lock (_planComputionCache)
            {
                if (_planComputionCache.ContainsKey(id))
                {
                    _planComputionCache[id].OperateTime = DateTime.Now;
                    return _planComputionCache[id];
                }
                var planLibInfo = _lotteryPlanLibRepository.Single(p => p.Id == id);
                if (planLibInfo == null)
                {
                    string msg = string.Format("不存在Id为{0}计划，请检查您输入的url是否正确", id);
                    LogHelper.Logger.Error(msg);
                    throw new Exception(msg);
                }
                var planComputtionList = sscLotteryEngine.ConvertPCListFromXml(XElement.Parse(planLibInfo.PlanComputionInfo));
                _planComputionCache.Add(id, new PlanComputionData(planComputtionList, planLibInfo));
                _planComputionCache[id].OperateTime = DateTime.Now;
                return _planComputionCache[id];
            }
        }
    }
}