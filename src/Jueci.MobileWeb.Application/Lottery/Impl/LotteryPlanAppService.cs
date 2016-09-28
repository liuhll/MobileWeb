using System.Collections.Generic;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery.Models.Transfer;
using Jueci.MobileWeb.Ssc;

namespace Jueci.MobileWeb.Lottery.Impl
{
    public class LotteryPlanAppService : ILotteryPlanAppService
    {
        protected readonly ILotteryPlanProcessor _lotteryPlanProcessor;

        public LotteryPlanAppService(ILotteryPlanProcessor lotteryPlanProcessor)
        {
            _lotteryPlanProcessor = lotteryPlanProcessor;
        }

        public  IList<UserPlanInfo> GetUserPlanInfos(string id,CPType cpType)
        {
            return _lotteryPlanProcessor.GetUserPlanInfos(id, cpType);
        }
    }
}