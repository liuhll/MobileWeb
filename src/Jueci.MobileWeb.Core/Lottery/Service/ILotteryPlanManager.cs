using System.Collections.Generic;
using Abp.Dependency;
using Camew.Lottery;
using Camew.Lottery.AppService;

namespace Jueci.MobileWeb.Lottery.Service
{
    public interface ILotteryPlanManager : ISingletonDependency
    {
        bool UpdateUserLotteryPlan(string id, List<PlanComputionInfo> planInfos);


        List<PlanComputionInfo> GetComputionInfos(string id, LotteryEngine sscLotteryEngine,ref bool isNeedUpdateCache);

    }
}