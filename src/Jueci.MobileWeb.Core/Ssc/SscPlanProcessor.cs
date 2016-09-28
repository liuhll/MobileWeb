using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Lottery.Service;

namespace Jueci.MobileWeb.Ssc
{
    public class SscPlanProcessor : LotteryPlanProcessor ,ISscPlanProcessor
    {
        public SscPlanProcessor(ILotteryServiceManager lotteryServiceManager, ILotteryPlanManager lotteryPlanManager) : base(lotteryServiceManager, lotteryPlanManager)
        {
        }
    }
}