using System;
using System.Collections.Generic;
using System.Linq;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Common;
using Jueci.MobileWeb.Common.Enums;
using Jueci.MobileWeb.Lottery.Models.Transfer;
using Jueci.MobileWeb.Lottery.Service;

namespace Jueci.MobileWeb.Lottery
{
    public class LotteryPlanProcessor : ILotteryPlanProcessor
    {

        private readonly ILotteryServiceManager _lotteryServiceManager;
        private readonly ILotteryPlanManager _lotteryPlanManager;

        public LotteryPlanProcessor(
            ILotteryServiceManager lotteryServiceManager,
            ILotteryPlanManager lotteryPlanManager)
        {
            _lotteryServiceManager = lotteryServiceManager;
            _lotteryPlanManager = lotteryPlanManager;
        }


        public ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id, CPType cpType)
        {
            var planComptionInfoList = UpdateComptionInfo(id, cpType);

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
                    GuessResultList = p.DMSMResultList.Take(p.DMSMResultList.Count - 1)
                                      .Select(x => x.Result > 0 ? RightOrWrongEnum.Right : RightOrWrongEnum.Wrong)
                                      .ToList(),
                    EndIndex = p.PlanParameter.PlanCycle > 1 ? dsRet.ActualEndTermIndex + 1 : (int?)null,
                    GuessPercent = p.DMSMResultList.Count(x => x.Result > 0) / (float)(p.DMSMResultList.Count - 1)

                };
            }).ToList();
            return new ResultMessage<IList<UserPlanInfo>>(userPlanInfo);
        }



        public ResultMessage<NewLottery> GetNewLottery(string id, CPType cpType)
        {
            var sscLotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            var latestCPDataInfo = sscLotteryEngine.GetLatestCPDataInfo();
            var nowTime = DateTime.Now;
            var newLottery = new NewLottery()
            {
                CurrentPeriod = latestCPDataInfo.Data.ID,
                LotteryResult = latestCPDataInfo.Data.Data,
                NextPeriod = latestCPDataInfo.NextCPDataID,
                NextPeriodTimePoint = latestCPDataInfo.NextCPDataOpenTime,

                NextPeriodTime = (int) (latestCPDataInfo.NextCPDataOpenTime > nowTime ? (latestCPDataInfo.NextCPDataOpenTime - nowTime).TotalSeconds : 0),
            };
            return new ResultMessage<NewLottery>(newLottery);

        }

        public ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, CPType cpType)
        {
            var planComptionInfoList = UpdateComptionInfo(id, cpType);
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
                    RightTimes = pret.CycleTrue,
                    PlanDetails = pc.DMSMResultList.Select(dmsmResultItem => new PlanDetailList()
                    {
                        ////064-066期|1 2 5 7|2|065|22049|对,
                        CycleName = dmsmResultItem.GetPlanRegionString(),                 
                        LotteryResult = dmsmResultItem.GetDMSMForecastString(),
                        DsType = pc.Plan.DSType ,
                        EndIndex = dmsmResultItem.ActualEndTermIndex + 1,
                        CurrentCycleName = dmsmResultItem.GetPlanActualEndTerm(),
                        GuessValue = dmsmResultItem.ActualEndData,
                        RightOrWrong = dmsmResultItem.GetPlanResultString(),

                    }).ToList(),
                  
                };
            }).ToList();
            return new ResultMessage<IList<UserPlanDetail>>(userPlanDetail);
        }


        private List<PlanComputionInfo> UpdateComptionInfo(string id, CPType cpType)
        {
            var sscLotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            bool isNeedUpdateCache = false;
            var planComptionInfoList = _lotteryPlanManager.GetComputionInfos(id, sscLotteryEngine, ref isNeedUpdateCache);

            if (isNeedUpdateCache)
            {
                _lotteryPlanManager.UpdateUserLotteryPlan(id, planComptionInfoList);
            }
            sscLotteryEngine.ComputeLotteryPlans(planComptionInfoList);
            return planComptionInfoList;
        }

        //private IList<UserPlanInfo> GetUserPlanInfos(List<PlanComputionInfo> planComptionInfoList)
        //{

        //}

        private bool IsHaveDMSMResult(List<PlanComputionInfo> planComptionInfoList)
        {
            if (planComptionInfoList.Any(p => p.DMSMResultList == null || p.DMSMResultList.Count == 0))
            {
                return false;
            }
            return true;
        }
    }
}