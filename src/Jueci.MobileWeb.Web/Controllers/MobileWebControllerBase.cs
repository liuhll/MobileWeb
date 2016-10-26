using Abp.Web.Mvc.Controllers;
using Jueci.MobileWeb.Common.Tools;

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
            //Session.Timeout = ConfigHelper.GetIntValues("SessionTime");
        }

        protected void AddSessionValue<T>(string key, T value)
        {
            Session.Add(key,value);
        }

        protected T GetSessionValue<T>(string key)
        {
            if (Session[key] != null)
            {
                return (T)Session[key];
            }
            return default(T);
        }
    }
}