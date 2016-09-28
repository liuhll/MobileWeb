using System.Collections.Generic;
using System.Linq;
using Camew.Lottery;
using Camew.Lottery.AppService;
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


        public IList<UserPlanInfo> GetUserPlanInfos(string id, CPType cpType)
        {
            var sscLotteryEngine = _lotteryServiceManager.GetServiceManager(cpType).LotteryEngine;
            bool isNeedUpdateCache = false;
            var planComptionInfoList = _lotteryPlanManager.GetComputionInfos(id, sscLotteryEngine, ref isNeedUpdateCache);

            if (isNeedUpdateCache)
            {
                _lotteryPlanManager.UpdateUserLotteryPlan(id, planComptionInfoList);
            }
            sscLotteryEngine.ComputeLotteryPlans(planComptionInfoList);
            return GetUserPlanInfos(planComptionInfoList);
        }

        private IList<UserPlanInfo> GetUserPlanInfos(List<PlanComputionInfo> planComptionInfoList)
        {
            return planComptionInfoList.Select(p => {

                if (p.DMSMResultList == null || p.DMSMResultList.Count == 0)
                {
                    return null;
                }
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
        }
    }
}