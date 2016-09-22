using Abp.Web.Mvc.Controllers;

namespace Jueci.MobileWeb.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class MobileWebControllerBase : AbpController
    {
        protected MobileWebControllerBase()
        {
            LocalizationSourceName = MobileWebConsts.LocalizationSourceName;
        }
    }
}