using System.Collections.Generic;
using System.Web.Http;
using Abp.WebApi.Controllers;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Lottery.Models.Transfer;

namespace Jueci.MobileWeb.Api.Controllers
{
    [RoutePrefix("api/ssc")]
    public class SscController :AbpApiController
    {
        private readonly ILotteryPlanAppService _lotteryPlanAppService;

        public SscController(ILotteryPlanAppService lotteryPlanAppService)
        {
            _lotteryPlanAppService = lotteryPlanAppService;
        }

        /// <summary>
        /// 获取用户计划详情
        /// </summary>
        /// <param name="id">用户计划详情Id</param>
        /// <returns>返回用户计划信息</returns>
        /// <remarks>返回用户的计划详情</remarks>      
        [HttpGet]
        [Route("{id:string}")]
        public IList<UserPlanInfo> GetUserPlanInfos(string id)
        {
            return _lotteryPlanAppService.GetUserPlanInfos("201600927001", CPType.cqssc);
        }

        //[HttpGet]
        //[Route("plan/{id:string}")]      
        //public IList<UserPlanInfo> GetUserPlanInfos(string id)
        //{
        //    return _lotteryPlanAppService.GetUserPlanInfos("201600927001", CPType.cqssc);
        //}


    }
}