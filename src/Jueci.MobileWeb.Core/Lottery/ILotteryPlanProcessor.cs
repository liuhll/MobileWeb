using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Lottery
{
    public interface ILotteryPlanProcessor : ITransientDependency
    {
   
        IList<UserPlanInfo> GetUserPlanInfos(string id,CPType cpType);

    }
}