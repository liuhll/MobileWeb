using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Abp.Domain.Repositories;
using Abp.Logging;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Common;
using Jueci.MobileWeb.Common.Enums;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Lottery.Models.Transfer;
using Jueci.MobileWeb.Lottery.Policy;
using Jueci.MobileWeb.Lottery.Service;
using Newtonsoft.Json;

namespace Jueci.MobileWeb.Lottery
{
    public class LotteryPlanProcessor : ILotteryPlanProcessor
    {

        private readonly ILotteryServiceManager _lotteryServiceManager;
        private readonly ILotteryPlanManager _lotteryPlanManager;

        private readonly IRepository<LotteryPlanLib, string> _lotteryPlanLibRepository;

        public LotteryPlanProcessor(
            ILotteryServiceManager lotteryServiceManager,
            ILotteryPlanManager lotteryPlanManager,
            IRepository<LotteryPlanLib, string> lotteryPlanLibRepository)
        {
            _lotteryServiceManager = lotteryServiceManager;
            _lotteryPlanManager = lotteryPlanManager;
            _lotteryPlanLibRepository = lotteryPlanLibRepository;
        }


        public ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id, string vcode, CPType cpType, bool isRepeatedValid)
        {
            var lotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            var lotteryPlanLib = _lotteryPlanManager.GetComputionData(id, lotteryEngine).LotteryPlanLib;

            ResultMessage<IList<UserPlanInfo>> result = null;

            return !ValidAccessCodeLegal(lotteryPlanLib, vcode, isRepeatedValid, out result) ?
                result : GetUserPlanInfoList(id,(int)lotteryPlanLib.State,cpType);
        }

        public ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id, CPType cpType, bool isNeedValidVcode)
        {
            var lotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            var lotteryPlanLib = _lotteryPlanManager.GetComputionData(id, lotteryEngine).LotteryPlanLib;
            if (lotteryPlanLib.IsNeedAccessRight && isNeedValidVcode)
            {
                return new ResultMessage<IList<UserPlanInfo>>(ResultCode.NotAllowed, MessageTips.NoAccessRight);
            }

            return GetUserPlanInfoList(id, (int)lotteryPlanLib.State, cpType);

        }

        public ResultMessage<NewLottery> GetNewLottery(CPType cpType)
        {

            var sscLotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            var latestCPDataInfo = sscLotteryEngine.GetLatestCPDataInfo();
            if (latestCPDataInfo.Data.Data == null)
            {
                return new ResultMessage<NewLottery>(ResultCode.Fail, MessageTips.WaitingServiceStart);
            }
            var nowTime = DateTime.Now;
            var newLottery = new NewLottery()
            {
                CurrentPeriod = latestCPDataInfo.Data.ID,
                LotteryResult = latestCPDataInfo.Data.Data.Split(',').Select(i => Convert.ToInt32(i)).ToList(),
                NextPeriod = latestCPDataInfo.NextCPDataID,
                NextPeriodTimePoint = latestCPDataInfo.NextCPDataOpenTime,

                NextPeriodTime = (int)(latestCPDataInfo.NextCPDataOpenTime > nowTime ? (latestCPDataInfo.NextCPDataOpenTime - nowTime).TotalSeconds : 0),
            };
            return new ResultMessage<NewLottery>(newLottery);

        }

        public ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, CPType cpType)
        {
            int libState;
            var planComptionInfoList = UpdateComptionInfo(id, cpType,out libState);
            if (!IsHaveDMSMResult(planComptionInfoList))
            {
                return new ResultMessage<IList<UserPlanDetail>>(ResultCode.Fail, MessageTips.NoDmsmResult);
            }
            var userPlanDetail = planComptionInfoList.Select(pc =>
            {
                var pret = pc.GetResultProperties();
                return new UserPlanDetail()
                {
                    PlanName = pc.Plan.Name,
                    RightRate = pret.Accuracy,
                    MaxAlwaysRight = pret.MaxLianDui,
                    MaxAlwaysWrong = pret.MaxLianCuo,
                    CurrentROrW = pret.CurrentLianDui,
                    // :todo
                    RightTimes = libState >1 ? pret.CycleTrue : null,
                    PlanDetails = pc.DMSMResultList.Select(dmsmResultItem => new PlanDetailList()
                    {
                        ////064-066期|1 2 5 7|2|065|22049|对,
                        CycleName = dmsmResultItem.GetPlanRegionString(),
                        LotteryResult = dmsmResultItem.ActualEndData, //
                        DsType = pc.Plan.DSType,
                        EndIndex = dmsmResultItem.ActualEndTermIndex + 1,
                        CurrentCycleName = dmsmResultItem.GetPlanActualEndTerm(),
                        GuessValue = dmsmResultItem.GetDMSMForecastString(),
                        RightOrWrong = dmsmResultItem.GetPlanResultString(),
                    }).ToList(),
                };
            }).ToList();
            return new ResultMessage<IList<UserPlanDetail>>(userPlanDetail);
        }

        public ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, string vcode, CPType cpType, bool isRepeatedValid)
        {
            var lotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            var lotteryPlanLib = _lotteryPlanManager.GetComputionData(id, lotteryEngine).LotteryPlanLib;
            ResultMessage<IList<UserPlanDetail>> result = null;
            return !ValidAccessCodeLegal(lotteryPlanLib, vcode, isRepeatedValid, out result) ?
                   result : GetUserPlanDetail(id, cpType);
        }

        public ResultMessage<UserPlanDetail> GetUserPlanDetailPosition(string id, string planName, CPType cpType)
        {
            int libState;
            var planComptionInfoList = UpdateComptionInfo(id, cpType, out libState);
            if (!IsHaveDMSMResult(planComptionInfoList))
            {
                return new ResultMessage<UserPlanDetail>(ResultCode.Fail, MessageTips.NoDmsmResult);
            }
            var planDetail = planComptionInfoList.FirstOrDefault(pc => pc.Plan.Name.Equals(planName));
            if (planDetail == null)
            {
                return new ResultMessage<UserPlanDetail>(ResultCode.Fail, string.Format(MessageTips.NoThisPlanDetail, planName));
            }

            var pret = planDetail.GetResultProperties();

            var userPlanDetail = new UserPlanDetail()
            {
                PlanName = planDetail.Plan.Name,
                RightRate = pret.Accuracy,
                MaxAlwaysRight = pret.MaxLianDui,
                MaxAlwaysWrong = pret.MaxLianCuo,
                CurrentROrW = pret.CurrentLianDui,
                // :todo
                RightTimes = pret.CycleTrue,
                PlanDetails = planDetail.DMSMResultList.Select(dmsmResultItem => new PlanDetailList()
                {
                    ////064-066期|1 2 5 7|2|065|22049|对,
                    CycleName = dmsmResultItem.GetPlanRegionString(),
                    LotteryResult = dmsmResultItem.ActualEndData,//
                    DsType = planDetail.Plan.DSType,
                    EndIndex = dmsmResultItem.ActualEndTermIndex + 1,
                    CurrentCycleName = dmsmResultItem.GetPlanActualEndTerm(),
                    GuessValue = dmsmResultItem.GetDMSMForecastString(),
                    RightOrWrong = dmsmResultItem.GetPlanResultString(),

                }).ToList(),

            };

            return new ResultMessage<UserPlanDetail>(userPlanDetail);

        }

        public ResultMessage<bool> UpdateUserPlanCache(CPType cpType, PlanCacheArgs planCacheArgs)
        {
            LogHelper.Logger.Info(string.Format(MessageTips.StartCallApiLog, "UpdateUserPlanCache", JsonConvert.SerializeObject(planCacheArgs)));
            //检查请求的合法性
            var planlibPolicy = new UpdateUserPlanLibPolicy(planCacheArgs);

            if (!planlibPolicy.IsValidTime())
            {

                LogHelper.Logger.Warn(MessageTips.NotValidTime);
                return new ResultMessage<bool>(ResultCode.Fail, MessageTips.NotValidTime, false);
            }
            if (!planlibPolicy.IsLegalSign())
            {

                LogHelper.Logger.Warn(MessageTips.NotLegalSign);
                return new ResultMessage<bool>(ResultCode.Fail, MessageTips.NotLegalSign, false);
            }

            var lotteryPlanLib =
                _lotteryPlanLibRepository.Single(p => p.UId == planCacheArgs.Uid && p.SId == planCacheArgs.Sid);
            var lotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;

            if (lotteryPlanLib == null)
            {
                return new ResultMessage<bool>(ResultCode.Fail, MessageTips.NoExitPlanLib, false);
            }

            var planComputionInfos = lotteryEngine.ConvertPCListFromXml(XElement.Parse(lotteryPlanLib.PlanComputionInfo));
            try
            {
                var data = _lotteryPlanManager.UpdateUserLotteryPlan(lotteryPlanLib.Id, planComputionInfos, lotteryPlanLib);
                LogHelper.Logger.Info(string.Format(MessageTips.EndCallApiLog, "UpdateUserPlanCache", data));
                return new ResultMessage<bool>(data);
            }
            catch (Exception e)
            {
                LogHelper.Logger.Error(e.Message);
                return new ResultMessage<bool>(ResultCode.ServiceError, e.Message, false);
            }
        }

        public ResultMessage<bool> IsNeedAccessRight(string id, CPType cpType)
        {
            var lotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            var lotteryPlanLib = _lotteryPlanManager.GetComputionData(id, lotteryEngine).LotteryPlanLib;
            return new ResultMessage<bool>(lotteryPlanLib.IsNeedAccessRight, MessageTips.NoAccessRight);
        }

        //public ResultMessage<List<PlanComputionInfo>> GetPlanComputionInfos(string id, CPType cpType)
        //{
        //    var planComputionInfos = UpdateComptionInfo(id, cpType);
        //    return new ResultMessage<List<PlanComputionInfo>>(planComputionInfos);
        //}

        public PlanLibTitle GetPlanLibTitle(string id, CPType cpType)
        {
            var lotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            var lotteryPlanLib = _lotteryPlanManager.GetComputionData(id, lotteryEngine).LotteryPlanLib;

            return new PlanLibTitle(cpType, lotteryPlanLib.State)
            {
                PlanTitle = lotteryPlanLib.TeamName,
                SubPlanTitle = lotteryPlanLib.Contact
            };
        }


        #region 私有方法
        private List<PlanComputionInfo> UpdateComptionInfo(string id, CPType cpType,out int libState)
        {
            var lotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
           
            var planComptionInfoList = _lotteryPlanManager.GetComputionInfos(id, lotteryEngine);
            var lotteryPlanLib = _lotteryPlanManager.GetComputionData(id, lotteryEngine).LotteryPlanLib;
            lotteryEngine.ComputeLotteryPlans(planComptionInfoList);
            libState = (int)lotteryPlanLib.State;
            return planComptionInfoList;
        }

        private bool ValidAccessCodeLegal<T>(LotteryPlanLib lotteryPlanLib, string vcode, bool isRepeatedValid, out ResultMessage<T> result)
        {
         

            if (lotteryPlanLib.IsNeedAccessRight && string.IsNullOrEmpty(vcode))
            {
                result = isRepeatedValid ?
                    new ResultMessage<T>(ResultCode.NotAllowed, MessageTips.NoAccessCode) :
                    new ResultMessage<T>(ResultCode.NotAllowed, MessageTips.NoAccessRight);
                return false;
            }
            if (lotteryPlanLib.IsNeedAccessRight && !lotteryPlanLib.VCode.Equals(vcode, StringComparison.OrdinalIgnoreCase))
            {
                result = isRepeatedValid ?
                    new ResultMessage<T>(ResultCode.NotAllowed, MessageTips.AccessCodeError) :
                    new ResultMessage<T>(ResultCode.NotAllowed, MessageTips.AccessCodeChange);

                return false;
            }
            result = new ResultMessage<T>(ResultCode.Success, "ValidAccessCode Success");
            return true;
        }

        private bool IsHaveDMSMResult(List<PlanComputionInfo> planComptionInfoList)
        {
            if (planComptionInfoList.Any(p => p.DMSMResultList == null || p.DMSMResultList.Count == 0))
            {
                return false;
            }
            return true;
        }

        private ResultMessage<IList<UserPlanInfo>> GetUserPlanInfoList(string id, int libState, CPType cpType)
        {
            var planComptionInfoList = UpdateComptionInfo(id, cpType,out libState);

            if (!IsHaveDMSMResult(planComptionInfoList))
            {
                return new ResultMessage<IList<UserPlanInfo>>(ResultCode.Fail, MessageTips.NoDmsmResult);
            }
            var userPlanInfo = planComptionInfoList.Select(p =>
            {
                var dsRet = p.DMSMResultList.Last();
                return new UserPlanInfo()
                {
                    Name = p.Plan.Name,
                    DsType = p.Plan.DSType,
                    PlanSection = dsRet.GetPlanRegionString(),
                    GuessValue = dsRet.Data.ToString(),
                    GuessResultList = GetGuessResultList(p, libState),
                    EndIndex = p.PlanParameter.PlanCycle > 1 ? dsRet.ActualEndTermIndex + 1 : (int?)null,
                    GuessPercent = p.DMSMResultList.Count(x => x.Result > 0) / (float)(p.DMSMResultList.Count - 1),

                };
            }).ToList();
            return new ResultMessage<IList<UserPlanInfo>>(userPlanInfo);
        }

        private static List<RightOrWrongEnum> GetGuessResultList(PlanComputionInfo p, int libState)
        {
            if (libState <= 1)
            {
                return p.DMSMResultList.Take(1)
                     .Select(x => x.Result > 0 ? RightOrWrongEnum.Right : RightOrWrongEnum.Wrong)
                     .ToList();
            }
            return p.DMSMResultList.Take(p.DMSMResultList.Count - 1)
                .Select(x => x.Result > 0 ? RightOrWrongEnum.Right : RightOrWrongEnum.Wrong)
                .ToList();
        }

        #endregion

    }
}