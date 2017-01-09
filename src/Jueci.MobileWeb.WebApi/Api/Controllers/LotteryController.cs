using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.Web.Security.AntiForgery;
using Abp.WebApi.Controllers;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Common.Tools;
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
        /// <param name="cpType">彩票类型</param>
        /// <returns>返回用户计划信息</returns>
        /// <remarks>返回用户的计划详情</remarks>         
        [HttpGet]
        [Route("{id:string}")]
        public ResultMessage<IList<UserPlanInfo>> UserPlan(string id,string cpType)
        {
            return _lotteryPlanAppService.GetUserPlanInfos(id,ConvertHelper.StringToEnum<CPType>(cpType));
        }

        /// <summary>
        /// 获取下一期开奖信息
        /// </summary>
        /// <param name="cpType"></param>
        /// <returns></returns> 
        /// <remarks>获取下一期开奖信息</remarks>                 
        [HttpGet]
        //        [Route("api/lottery/clock")]
        public ResultMessage<NewLottery> Clock(string cpType)
        {
            return _lotteryPlanAppService.GetNewLottery(ConvertHelper.StringToEnum<CPType>(cpType));
        }

        /// <summary>
        /// 获取用户分享计划信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cpType"></param>
        /// <returns></returns>
        /// <remarks>获取用户分享计划信息详情</remarks> 
        [HttpGet]
        public ResultMessage<IList<UserPlanDetail>> UserPlanDetail(string id,string cpType)
        {
            return _lotteryPlanAppService.GetUserPlanDetail(id, ConvertHelper.StringToEnum<CPType>(cpType));
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
            CPType cpType ;
            switch (planCacheArgs.Sid)
            {
                case 1:
                    cpType= CPType.cqssc;
                    break;
                case 2:
                    cpType = CPType.pks;
                    break;
                case 3:
                    cpType = CPType.gdklsf;
                    break;
                case 4:
                    cpType = CPType.cqklsf;
                    break;            
                case 5:
                    cpType = CPType.jx11x5;
                    break;
                case 6:
                    cpType = CPType.gd11x5;
                    break;
                case 7:
                    cpType = CPType.sd11x5;
                    break;
                case 8:
                    cpType = CPType.jsks;
                    break;
                case 9:
                    cpType = CPType.kl8;
                    break;
                default:
                    throw new Exception("没有您指定的服务类型的彩种！");
            }
            return _lotteryPlanAppService.UpdateUserPlanCache(cpType, planCacheArgs);
        }


    }
}