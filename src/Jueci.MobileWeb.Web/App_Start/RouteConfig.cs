using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jueci.MobileWeb.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ASP.NET Web API Route Config

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            //routes.MapHttpRoute(
            //    name: "BehaviorApi",
            //    routeTemplate: "api/{controller}/{id}/{behavior}",
            //    defaults: new { id = RouteParameter.Optional, behavior = RouteParameter.Optional }
            //    );

            routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
                );


            routes.MapRoute(
                name: "Default",
                url: "app/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
