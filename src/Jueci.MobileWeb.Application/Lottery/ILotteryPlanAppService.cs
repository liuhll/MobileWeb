using System.Collections.Generic;
using Abp.Application.Services;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Lottery
{
    public interface ILotteryPlanAppService : IApplicationService
    {
        IList<UserPlanInfo> GetUserPlanInfos(string id, CPType cpType);
    }
}