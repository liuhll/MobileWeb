using System.Collections.Generic;
using System.Web.Http;
using Abp.WebApi.Controllers;
using Camew.Lottery;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Api.Controllers
{
    /// <summary>
    /// 重庆时时彩计划分享计划接口
    /// </summary>
    /// <remarks>
    /// 重庆时时彩计划分享计划接口
    /// </remarks>
    [RoutePrefix("api/ssc")]
    public class SscController :AbpApiController
    {
        private readonly ILotteryPlanAppService _lotteryPlanAppService;

        /// <summary>
        /// 重庆时时彩计划分享计划接口
        /// </summary>
        public SscController(ILotteryPlanAppService lotteryPlanAppService)
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
            return _lotteryPlanAppService.GetUserPlanInfos("201600927001", CPType.cqssc);
        }

        /// <summary>
        /// 获取下一期开奖信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        /// <remarks>获取下一期开奖信息</remarks>                 
        [HttpGet]   
//        [Route("api/ssc/clock")]
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
            return _lotteryPlanAppService.GetUserPlanDetail("201600927001", CPType.cqssc);
        }

        //[HttpGet]
        //[Route("plan/{id:string}")]      
        //public IList<UserPlanInfo> GetUserPlanInfos(string id)
        //{
        //    return _lotteryPlanAppService.GetUserPlanInfos("201600927001", CPType.cqssc);
        //}

    }
}