using System.Collections.Generic;
using Abp.Application.Services;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Lottery
{
    public interface ILotteryPlanAppService : IApplicationService
    {
        ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id,string vcode, CPType cpType,bool isRepeatedValid = false);

        ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id, CPType cpType, bool isNeedValidVcode = false);

        ResultMessage<NewLottery> GetNewLottery(CPType cpType);

        ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, CPType cpType);

        ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, string vcode, CPType cpType, bool isRepeatedValid = false);

        ResultMessage<UserPlanDetail> GetUserPlanDetailPosition(string id, string planName, CPType cpType);

        ResultMessage<bool> UpdateUserPlanCache(CPType cpType, PlanCacheArgs planCacheArgs);

        //ResultMessage<List<PlanComputionInfo>> GetPlanComputionInfos(string id, CPType cpType);
        ResultMessage<bool> IsNeedAccessRight(string id,CPType cpType);

        PlanLibTitle GetPlanLibTitle(string id, CPType cpType);
    }
}