using System.Collections.Generic;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Lottery.Models;
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

        public ResultMessage<IList<UserPlanInfo>> GetUserPlanInfos(string id,CPType cpType)
        {
            return _lotteryPlanProcessor.GetUserPlanInfos(id, cpType);
        }

        public ResultMessage<NewLottery> GetNewLottery(CPType cpType)
        {
            return _lotteryPlanProcessor.GetNewLottery(cpType);
        }

        public ResultMessage<IList<UserPlanDetail>> GetUserPlanDetail(string id, CPType cpType)
        {
            return _lotteryPlanProcessor.GetUserPlanDetail(id, cpType);
        }

        public ResultMessage<UserPlanDetail> GetUserPlanDetailPosition(string id, string planName, CPType cpType)
        {
            return _lotteryPlanProcessor.GetUserPlanDetailPosition( id, planName, cpType);
        }

        public ResultMessage<bool> UpdateUserPlanCache(CPType cpType, PlanCacheArgs planCacheArgs)
        {
            return _lotteryPlanProcessor.UpdateUserPlanCache(cpType, planCacheArgs);
        }

        //public ResultMessage<List<PlanComputionInfo>> GetPlanComputionInfos(string id, CPType cpType)
        //{
        //    return _lotteryPlanProcessor.GetPlanComputionInfos(id, cpType);
        //}
    }
}