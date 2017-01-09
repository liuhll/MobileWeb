using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class Jx11x5Controller : PlanController
    {
        // GET: Jx11x5

        public Jx11x5Controller(ILotteryPlanAppService planAppService) : base(planAppService, CPType.jx11x5)
        {
        }
    }
}