using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Lottery
{
    public interface ILotteryPlanProcessor : ITransientDependency
    {

        ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id,CPType cpType);

        ResultMessage<NewLottery> GetNewLottery(CPType cpType);

        ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, CPType cpType);

        ResultMessage<UserPlanDetail> GetUserPlanDetailPosition(string id, string planName, CPType cpType);

        ResultMessage<bool> UpdateUserPlanCache(CPType cpType, PlanCacheArgs planCacheArgs);

       // ResultMessage<List<PlanComputionInfo>> GetPlanComputionInfos(string id, CPType cpType);
    }
}