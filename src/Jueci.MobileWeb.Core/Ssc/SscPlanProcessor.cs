using Abp.Domain.Repositories;
using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Lottery.Service;

namespace Jueci.MobileWeb.Ssc
{
    public class SscPlanProcessor : LotteryPlanProcessor ,ISscPlanProcessor
    {
        public SscPlanProcessor(ILotteryServiceManager lotteryServiceManager, 
            ILotteryPlanManager lotteryPlanManager,
            IRepository<LotteryPlanLib, string> lotteryPlanLibRepository) 
            : base(lotteryServiceManager, lotteryPlanManager, lotteryPlanLibRepository)
        {
        }
    }
}