using System.Web.Mvc;

namespace Jueci.MobileWeb.Web.Controllers
{
    public class HomeController : MobileWebControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}