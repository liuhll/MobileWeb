﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Abp.Web.Security.AntiForgery;
using Jueci.MobileWeb.Lottery;
using Camew.Lottery;
using Jeuci.SalesSystem.Entities.Common;
using Jueci.MobileWeb.Common.Enums;
using Jueci.MobileWeb.Common.Tools;
using Jueci.MobileWeb.Lottery.Models.Transfer;
using Jueci.MobileWeb.Web.Models.Common;
using Jueci.MobileWeb.Web.Models.PlanShare;
using Jueci.MobileWeb.Web.Models.UserPlanDetail;
using Jueci.MobileWeb.Web.Views;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class PlanController : MobileWebControllerBase
    {
        private readonly ILotteryPlanAppService _planAppService;

        private readonly CPType _cpType;

        public PlanController(ILotteryPlanAppService planAppService, CPType cpType)
        {
            _planAppService = planAppService;
            _cpType = cpType;
            ViewBag.CpType = _cpType.ToString();
 
        }

     

        // GET: Ssc
        public ActionResult Planshare(string id)
        {
            var vcode = GetSessionValue<string>(id);
            ResultMessage<IList<UserPlanInfo>> userPlanInfoResult;
            try
            {
                userPlanInfoResult = !string.IsNullOrEmpty(vcode) ?
                    _planAppService.GetUserPlanInfos(id, vcode, _cpType) :
                    _planAppService.GetUserPlanInfos(id, _cpType, true);
            }
            catch (Exception e)
            {
                //return new HttpStatusCodeResult(404, string.Format("不存在id为{0}的计划库", id));
                return Redirect("http://plan.camew.com/404.html");
            }

            ViewBag.OfficialWebsite = ConfigHelper.GetValuesByKey("OfficialWebsite");
            ViewBag.PlanId = id;

            var planTitleInfo = _planAppService.GetPlanLibTitle(id, _cpType);
            ViewBag.PlanTitle = planTitleInfo.PlanTitle;
            ViewBag.SubPlanTitle = planTitleInfo.SubPlanTitle;
            ViewBag.PlanState = planTitleInfo.PlanLibState;
            SetDownloadAppSite();
            if (userPlanInfoResult.Code == ResultCode.NotAllowed)
            {
                ViewBag.ReturnUrl = Request.RawUrl.Substring(1);
                ViewBag.Message = userPlanInfoResult.Msg;
                return View("~/Views/Plan/PlanAccessCode.cshtml");
            }
            if (userPlanInfoResult.Code != ResultCode.Success)
            {
                return Redirect("http://plan.camew.com/404.html");
   
            }
            return View("~/Views/Plan/Planshare.cshtml", userPlanInfoResult.Data);
        }

        [System.Web.Mvc.HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        public JsonResult Planshare([FromBody]AccessCodeViewModel model)
        {
            var userPlanInfoResult = _planAppService.GetUserPlanInfos(model.Id, model.Vcode, _cpType, true);

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
                var result = _planAppService.IsNeedAccessRight(id, _cpType);
                if (result.Data)
                {
                    ViewBag.Message = result.Msg;
                    ViewBag.ReturnUrl = Request.RawUrl.Substring(1);
                    return View("~/Views/Plan/PlanAccessCode.cshtml");
                }
            }
            var userPlanDetail = string.IsNullOrEmpty(vcode) ?
                _planAppService.GetUserPlanDetail(id, _cpType) :
                _planAppService.GetUserPlanDetail(id, vcode, _cpType, true);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                return new HttpNotFoundResult(userPlanDetail.Msg);
            }
            ViewBag.PlanId = id;
            ViewBag.TabIndex = tabIndex;
            return View("~/Views/Plan/PlanDetails.cshtml", userPlanDetail.Data);
        }

        public PartialViewResult Clock(ClockPage clockPage)
        {
            var newLotteryResult = _planAppService.GetNewLottery(_cpType);
            switch (clockPage)
            {
                case ClockPage.HomePage:
                    return PartialView("~/Views/Plan/_Clock.cshtml", newLotteryResult.Data);
                case ClockPage.PlanningDetail:
                    return PartialView("~/Views/Plan/_PlanningDetailClock.cshtml", newLotteryResult.Data);
                default:
                    return PartialView("~/Views/Plan/_Clock.cshtml", newLotteryResult.Data);
            }


        }

        public PartialViewResult UserPlanInfoList(string id)
        {
            var userPlanInfo = _planAppService.GetUserPlanInfos(id, _cpType);
            if (userPlanInfo.Code != ResultCode.Success)
            {
                throw new Exception("get new planinfo error!");
            }
            ViewBag.PlanId = id;
            return PartialView("~/Views/Plan/_UserPlanInfoList.cshtml", userPlanInfo.Data);
        }

        [Obsolete]
        public PartialViewResult UserPlanDetailClock(string id, string planName, int tabIndex)
        {
            var userPlanDetail = _planAppService.GetUserPlanDetailPosition(id, planName, _cpType);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                throw new Exception("get  UserPlanDetailClock error!");
            }
            return PartialView("~/Views/Plan/_UserPlanDetailClock.cshtml", new UserPlanDetailClock(tabIndex, userPlanDetail.Data));
        }

        [Obsolete]
        public PartialViewResult UserPlanDetailInfo(string id, string planName)
        {
            var userPlanDetail = _planAppService.GetUserPlanDetailPosition(id, planName, _cpType);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                throw new Exception("get  UserPlanDetailClock error!");
            }
            return PartialView("~/Views/Plan/_UserPlanDetailInfo.cshtml", userPlanDetail.Data);
        }

        [Obsolete]
        public PartialViewResult UserPlanDetailList(string id, string planName)
        {
            var userPlanDetail = _planAppService.GetUserPlanDetailPosition(id, planName, _cpType);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                throw new Exception("get  UserPlanDetailClock error!");
            }
            return PartialView("~/Views/Plan/_UserPlanDetailList.cshtml", userPlanDetail.Data);
        }

        public ActionResult UserPlanDetailContent(string id, int currentTabIndex)
        {
            string vcode = GetSessionValue<string>(id);
            if (string.IsNullOrEmpty(vcode))
            {
                var result = _planAppService.IsNeedAccessRight(id, _cpType);
                if (result.Data)
                {
                    string returnUrl = string.Format("app/{0}/plandetails/{1}?tabIndex={2}", _cpType.ToString(), id, currentTabIndex);
                    return Json(new AccessRightResult(true, result.Msg, returnUrl), JsonRequestBehavior.AllowGet);
                }
            }
            var userPlanDetail = string.IsNullOrEmpty(vcode)
                ? _planAppService.GetUserPlanDetail(id, _cpType)
                : _planAppService.GetUserPlanDetail(id, vcode, _cpType, true);
            if (userPlanDetail.Code != ResultCode.Success)
            {
                return new HttpNotFoundResult(userPlanDetail.Msg);
            }
            ViewBag.PlanId = id;
            ViewBag.TabIndex = currentTabIndex;
            return PartialView("~/Views/Plan/_UserPlanDetailContent.cshtml", userPlanDetail.Data);
        }


        private void SetDownloadAppSite()
        {
            if (BrowerChecker.IsMoblie(Request))
            {
                if (BrowerChecker.IsIphone(Request))
                {
                    if (BrowerChecker.IsWeChatBrower(Request))
                    {
                        switch (_cpType)
                        {
                            case CPType.pks:
                                ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("IphonePksPic");
                                break;
                            case CPType.cqssc:
                                ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("IphoneSscPic");
                                break;
                            default:
                                ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("IphoneComplexPic");
                                break;
                        }
                        return;
                    }

                    switch (_cpType)
                    {
                        case CPType.cqssc:
                            ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("IphoneAppStoreSsc");
                            break;
                        case CPType.pks:
                            ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("IphoneAppStorePks");
                            break;
                        default:
                            ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("IphoneAppStoreComplex");
                            break;
                    }
                }
                else
                {
                    switch (_cpType)
                    {
                        case CPType.cqssc:
                            ViewBag.DownloadApp = string.Format(ConfigHelper.GetValuesByKey("TencentAppStoreSite"), "ssc");
                            break;
                        case CPType.pks:
                            ViewBag.DownloadApp = string.Format(ConfigHelper.GetValuesByKey("TencentAppStoreSite"), "pks");
                            break;
                        default:
                            ViewBag.DownloadApp = string.Format(ConfigHelper.GetValuesByKey("TencentAppStoreSite"), "zyzj");
                            break;                          
                    }
                }
            }
            else
            {
                ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("OfficialWebsite");
            }

            //  ViewBag.DownloadApp = ConfigHelper.GetValuesByKey("OfficialWebsite");
        }

    }
}