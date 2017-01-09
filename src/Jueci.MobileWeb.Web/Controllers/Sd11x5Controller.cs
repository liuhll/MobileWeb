using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class Sd11x5Controller : PlanController
    {
        public Sd11x5Controller(ILotteryPlanAppService planAppService) : base(planAppService, CPType.sd11x5)
        {
        }
    }
}