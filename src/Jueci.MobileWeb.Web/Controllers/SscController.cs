using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jueci.MobileWeb.Lottery;
using Jueci.MobileWeb.Ssc;
using Camew.Lottery;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class SscController : MobileWebControllerBase
    {

        private readonly ILotteryPlanAppService _sscPlanAppService;

        public SscController(ILotteryPlanAppService sscPlanAppService)
        {
            _sscPlanAppService = sscPlanAppService;
        }

        // GET: Ssc
        public ActionResult Index()
        {
            //var obj= _sscPlanAppService.GetUserPlanInfos("201600927001",CPType.cqssc);   
            //return Json(obj,JsonRequestBehavior.AllowGet);
            return View();
        }

        public ActionResult PlanDetails()
        {
            return View();
        }
    }
}