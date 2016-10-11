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
using Jueci.MobileWeb.Web.Models.Common;
using Jueci.MobileWeb.Web.Models.UserPlanDetail;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class SscController : MobileWebControllerBase
    {

        private readonly ILotteryPlanAppService _sscPlanAppService;

        public SscController(ILotteryPlanAppService sscPlanAppService)
        {
            _sscPlanAppService = sscPlanAppService;
        }

        // GET: Ssc
        public ActionResult Index(string id)
        {
            //201600927001
            var userPlanInfo = _sscPlanAppService.GetUserPlanInfos(id, CPType.cqssc);
            if (userPlanInfo.Code != ResultCode.Success)
            {
                return new HttpNotFoundResult(userPlanInfo.Msg);
            }
            ViewBag.PlanId = id;
            return View(userPlanInfo.Data);
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