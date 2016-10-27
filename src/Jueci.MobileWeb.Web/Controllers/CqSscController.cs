using Jueci.MobileWeb.Lottery;
using Camew.Lottery;

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