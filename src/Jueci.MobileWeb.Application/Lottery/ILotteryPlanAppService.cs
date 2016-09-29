using System.Collections.Generic;
using Abp.Application.Services;
using Camew.Lottery;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Lottery
{
    public interface ILotteryPlanAppService : IApplicationService
    {
        ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id, CPType cpType);

        ResultMessage<NewLottery> GetNewLottery(string id, CPType cpType);

        ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, CPType cpType);
    }
}