using System.Collections.Generic;
using Abp.Dependency;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Lottery.Service.LotteryPlanManager;

namespace Jueci.MobileWeb.Lottery.Service
{
    public interface ILotteryPlanManager : ISingletonDependency
    {
        bool UpdateUserLotteryPlan(string id, List<PlanComputionInfo> planInfos, LotteryPlanLib lotteryPlanLib);


        List<PlanComputionInfo> GetComputionInfos(string id, LotteryEngine sscLotteryEngine,ref bool isNeedUpdateCache);

        PlanComputionData GetComputionData(string id, LotteryEngine sscLotteryEngine);
    }
}