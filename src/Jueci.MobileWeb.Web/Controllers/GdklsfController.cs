using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class GdklsfController : PlanController
    {
        public GdklsfController(ILotteryPlanAppService planAppService) : base(planAppService, CPType.gdklsf)
        {
        }
    }
}