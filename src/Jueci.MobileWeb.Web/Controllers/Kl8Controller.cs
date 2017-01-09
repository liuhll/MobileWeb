using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class Kl8Controller : PlanController
    {
        public Kl8Controller(ILotteryPlanAppService planAppService) : base(planAppService, CPType.kl8)
        {
        }
    }
}