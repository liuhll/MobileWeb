using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class JsksController : PlanController
    {
        // GET: Jsks

        public JsksController(ILotteryPlanAppService planAppService) : base(planAppService, CPType.jsks)
        {
        }
    }
}