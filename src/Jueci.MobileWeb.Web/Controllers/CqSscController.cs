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
    public class CqSscController : PlanController
    {
        public CqSscController(ILotteryPlanAppService planAppService) 
            : base(planAppService,CPType.cqssc)
        {
            ViewBag.PlanTitle = "重庆时时彩计划";
        }
    }
}