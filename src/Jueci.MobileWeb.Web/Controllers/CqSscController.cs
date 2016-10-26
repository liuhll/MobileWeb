using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Abp.Web.Models;
using Abp.Web.Security.AntiForgery;
using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Ssc;
using Camew.Lottery;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Common.Enums;
using Jueci.MobileWeb.Common.Tools;
using Jueci.MobileWeb.Lottery.Models.Transfer;
using Jueci.MobileWeb.Web.Models.Common;
using Jueci.MobileWeb.Web.Models.PlanShare;
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
            var vcode = GetSessionValue<string>(id);
            ResultMessage<IList<UserPlanInfo>> userPlanInfoResult;
            userPlanInfoResult = !string.IsNullOrEmpty(vcode) ?
                _sscPlanAppService.GetUserPlanInfos(id, vcode, CPType.cqssc) :
                _sscPlanAppService.GetUserPlanInfos(id, CPType.cqssc, true);

            ViewBag.OfficialWebsite = ConfigHelper.GetValuesByKey("OfficialWebsite");
            ViewBag.PlanId = id;
            if (userPlanInfoResult.Code == ResultCode.NotAllowed)
            {
                ViewBag.ReturnUrl = Request.RawUrl.Substring(1);
                ViewBag.Message = userPlanInfoResult.Msg;
                return View("PlanAccessCode");
            }
            if (userPlanInfoResult.Code != ResultCode.Success)
            {
                return new HttpNotFoundResult(userPlanInfoResult.Msg);
            }
            return View(userPlanInfoResult.Data);
        }

        [System.Web.Mvc.HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        public JsonResult Planshare([FromBody]AccessCodeViewModel model)
        {
            var userPlanInfoResult = _sscPlanAppService.GetUserPlanInfos(model.Id, model.Vcode, CPType.cqssc,true);

            if (userPlanInfoResult.Code != ResultCode.Success)
            {
                return Json(new AccessRightResult(false, userPlanInfoResult.Msg, model.ReturnUrl));
            }
            AddSessionValue(model.Id, model.Vcode);

            return Json(new AccessRightResult(true, userPlanInfoResult.Msg, model.ReturnUrl));

        }


        public ActionResult PlanDetails(string id, int tabIndex = 1)
        {
            //有session存在的情况下证明已经通过验证，不存在Session的情况下需要确认是否需要访问码，如果需要访问码，则跳转到访问码输入页面
            string vcode = GetSessionValue<string>(id);
            if (string.IsNullOrEmpty(vcode))
            {
                var result = _sscPlanAppService.IsNeedAccessRight(id, CPType.cqssc);
                if (result.Data)
                {
                    ViewBag.Message = result.Msg;
                    ViewBag.ReturnUrl = Request.RawUrl.Substring(1);
                    return View("PlanAccessCode");
                }
            }
            var userPlanDetail = string.IsNullOrEmpty(vcode) ? 
                _sscPlanAppService.GetUserPlanDetail(id, CPType.cqssc) :
                _sscPlanAppService.GetUserPlanDetail(id,vcode,CPType.cqssc,true);
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

        public PartialViewResult UserPlanDetailClock(string id, string planName, int tabIndex)
        {
            var userPlanDetail = _sscPlanAppService.GetUserPlanDetailPosition(id, planName, CPType.cqssc);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                throw new Exception("get  UserPlanDetailClock error!");
            }
            return PartialView("_UserPlanDetailClock", new UserPlanDetailClock(tabIndex, userPlanDetail.Data));
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
            return PartialView("_UserPlanDetailList", userPlanDetail.Data);
        }
    }
}