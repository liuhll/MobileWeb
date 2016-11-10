using Camew.Lottery;
using Jueci.MobileWeb.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class PksController : PlanController
    {
        public PksController(ILotteryPlanAppService planAppService) : base(planAppService, CPType.pks)
        {
            //ViewBag.PlanTitle = "掌赢专家";
            //ViewBag.SubPlanTitle = "北京Pk10计划";
        }
    }
}