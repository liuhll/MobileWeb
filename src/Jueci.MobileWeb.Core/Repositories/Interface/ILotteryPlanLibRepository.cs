using System.Collections.Generic;
using Camew.Lottery.AppService;

namespace Jueci.MobileWeb.Repositories
{
    public interface ILotteryPlanLibRepository
    {
        IList<PlanComputionInfo> GetComputionInfos(int id);
    }
}