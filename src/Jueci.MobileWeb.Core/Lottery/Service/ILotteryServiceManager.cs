using Abp.Dependency;
using Camew.Lottery;
using Camew.Lottery.AppService;

namespace Jueci.MobileWeb.Lottery.Service
{
    public interface ILotteryServiceManager : ISingletonDependency
    {
        LotteryServiceInfo GetServiceManager(CPType cpType);
    }
}