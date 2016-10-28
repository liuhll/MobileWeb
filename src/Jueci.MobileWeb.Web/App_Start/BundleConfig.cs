using System.Web.Optimization;

namespace Jueci.MobileWeb.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                    .Include("~/Abp/Lib/jquery.mobile.bootstrap/themes/Bootstrap.min.css", new CssRewriteUrlTransform())
                    .Include("~/Abp/Lib/jquery.mobile/jquery.mobile.structure-1.4.5.min.css", new CssRewriteUrlTransform())
                    .Include("~/Abp/Lib/jquery.mobile.bootstrap/themes/jquery.mobile.icons.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform())
                    
                );



            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js")
                    .Include(
                        "~/Scripts/json2.min.js",

                        "~/Scripts/jquery-2.1.4.min.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js",

                        "~/Abp/Lib/jquery.mobile/jquery.mobile-1.4.5.js",
                        "~/Scripts/jueci/mobilecheck.js",
                        "~/Scripts/jueci/loader.js"
                    )
                );
         
        }
    }
}