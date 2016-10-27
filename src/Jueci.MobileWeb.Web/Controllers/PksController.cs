using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class PksController : PlanController
    {
        public PksController(ILotteryPlanAppService planAppService) : base(planAppService, CPType.pks)
        {
            ViewBag.PlanTitle = "北京Pk10计划";
        }
    }
}