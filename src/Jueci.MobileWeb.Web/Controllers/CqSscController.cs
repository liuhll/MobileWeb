using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Ssc;
using Camew.Lottery;
using Jueci.MobileWeb.Common.Enums;
using Jueci.MobileWeb.Common.Tools;
using Jueci.MobileWeb.Web.Models.Common;
using Jueci.MobileWeb.Web.Models.UserPlanDetail;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class CqSscController : MobileWebControllerBase
    {

        private readonly ILotteryPlanAppService _sscPlanAppService;

        public CqSscController(ILotteryPlanAppService sscPlanAppService)
        {
            _sscPlanAppService = sscPlanAppService;
        }

        // GET: Ssc
        public ActionResult Planshare(string id)
        {
            //201600927001
            var userPlanInfoResult = _sscPlanAppService.GetUserPlanInfos(id, CPType.cqssc,true);

            ViewBag.OfficialWebsite = ConfigHelper.GetValuesByKey("OfficialWebsite");
            ViewBag.PlanId = id;
            if (userPlanInfoResult.Code == ResultCode.NotAllowed)
            {
                return View("PlanAccessCode");
            }
            if (userPlanInfoResult.Code != ResultCode.Success)
            {
                return new HttpNotFoundResult(userPlanInfoResult.Msg);
            }
            return View(userPlanInfoResult.Data);
        }

        public ActionResult PlanDetails(string id,int tabIndex = 1)
        {
            var userPlanDetail = _sscPlanAppService.GetUserPlanDetail(id, CPType.cqssc);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                return new HttpNotFoundResult(userPlanDetail.Msg);
            }
            ViewBag.PlanId = id;
            ViewBag.TabIndex = tabIndex;
            return View(userPlanDetail.Data);
        }

        public PartialViewResult Clock(ClockPage clockPage)
        {
            var newLotteryResult = _sscPlanAppService.GetNewLottery(CPType.cqssc);
            switch (clockPage)
            {
                case ClockPage.HomePage:
                    return PartialView("_Clock", newLotteryResult.Data);
                case ClockPage.PlanningDetail:
                    return PartialView("_PlanningDetailClock", newLotteryResult.Data);
                default:
                    return PartialView("_Clock", newLotteryResult.Data);
            }

           
        }

        public PartialViewResult UserPlanInfoList(string id)
        {
            var userPlanInfo = _sscPlanAppService.GetUserPlanInfos(id, CPType.cqssc);
            if (userPlanInfo.Code != ResultCode.Success)
            {
                throw new Exception("get new planinfo error!");
            }
            ViewBag.PlanId = id;
            return PartialView("_UserPlanInfoList", userPlanInfo.Data);
        }

        public PartialViewResult UserPlanDetailClock(string id,string planName, int tabIndex)
        {
            var userPlanDetail = _sscPlanAppService.GetUserPlanDetailPosition(id, planName, CPType.cqssc);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                 throw new Exception("get  UserPlanDetailClock error!");
            }
            return PartialView("_UserPlanDetailClock", new UserPlanDetailClock(tabIndex,userPlanDetail.Data));
        }

        public PartialViewResult UserPlanDetailInfo(string id, string planName)
        {
            var userPlanDetail = _sscPlanAppService.GetUserPlanDetailPosition(id, planName, CPType.cqssc);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                throw new Exception("get  UserPlanDetailClock error!");
            }
            return PartialView("_UserPlanDetailInfo", userPlanDetail.Data);
        }

        public PartialViewResult UserPlanDetailList(string id, string planName)
        {
            var userPlanDetail = _sscPlanAppService.GetUserPlanDetailPosition(id, planName, CPType.cqssc);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                throw new Exception("get  UserPlanDetailClock error!");
            }
            return PartialView("_UserPlanDetailList",userPlanDetail.Data);
        }
    }
}