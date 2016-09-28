using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Camew;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Lottery.Models;

namespace Jueci.MobileWeb.Repositories
{
    public interface ILotteryConfigRepository : IMobileWebRepositoryBase<LotteryConfig,string>
    {
        ResultObject<XElement> GetServiceInitConfig(CPType cpType);
       
    }
}