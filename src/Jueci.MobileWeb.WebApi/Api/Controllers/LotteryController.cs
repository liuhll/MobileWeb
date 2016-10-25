using System.Collections.Generic;
using System.Web.Http;
using Abp.Web.Security.AntiForgery;
using Abp.WebApi.Controllers;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Api.Controllers
{
    /// <summary>
    /// 重庆时时彩计划分享计划接口
    /// </summary>
    /// <remarks>
    /// 重庆时时彩计划分享计划接口
    /// </remarks>
    [RoutePrefix("api/lottery")]
    public class LotteryController :AbpApiController
    {
        private readonly ILotteryPlanAppService _lotteryPlanAppService;

        /// <summary>
        /// 重庆时时彩计划分享计划接口
        /// </summary>
        public LotteryController(ILotteryPlanAppService lotteryPlanAppService)
        {
            _lotteryPlanAppService = lotteryPlanAppService;
        }

        /// <summary>
        /// 获取用户计划信息
        /// </summary>
        /// <param name="id">用户分享的计划Id</param>
        /// <returns>返回用户计划信息</returns>
        /// <remarks>返回用户的计划详情</remarks>         
        [HttpGet]
        [Route("{id:string}")]
        public ResultMessage<IList<UserPlanInfo>> UserPlan(string id)
        {
            return _lotteryPlanAppService.GetUserPlanInfos(id, CPType.cqssc);
        }

        /// <summary>
        /// 获取下一期开奖信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        /// <remarks>获取下一期开奖信息</remarks>                 
        [HttpGet]
        //        [Route("api/lottery/clock")]
        public ResultMessage<NewLottery> Clock()
        {
            return _lotteryPlanAppService.GetNewLottery(CPType.cqssc);
        }

        /// <summary>
        /// 获取用户分享计划信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>获取用户分享计划信息详情</remarks> 
        [HttpGet]
        public ResultMessage<IList<UserPlanDetail>> UserPlanDetail(string id)
        {
            return _lotteryPlanAppService.GetUserPlanDetail(id, CPType.cqssc);
        }

        /// <summary>
        /// 更新用户计划缓存接口
        /// </summary>
        /// <param name="planCacheArgs"></param>
        /// <returns>是否更新成功</returns>
        /// <remarks>获取用户分享计划信息详情，当用户计划更改时，需要条用该接口，更新缓存中的计划信息</remarks> 
        [HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        public ResultMessage<bool> UpdateUserPlanCache([FromBody] PlanCacheArgs planCacheArgs)
        {
            return _lotteryPlanAppService.UpdateUserPlanCache(CPType.cqssc, planCacheArgs);
        }


    }
}