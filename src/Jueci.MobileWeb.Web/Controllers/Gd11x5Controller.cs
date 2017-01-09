using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class Gd11x5Controller : PlanController
    {
        // GET: Gd11x5

        public Gd11x5Controller(ILotteryPlanAppService planAppService) : base(planAppService, CPType.gd11x5)
        {
        }
    }
}