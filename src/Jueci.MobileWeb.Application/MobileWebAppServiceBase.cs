using Abp.Application.Services;

namespace Jueci.MobileWeb
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class MobileWebAppServiceBase : ApplicationService
    {
        protected MobileWebAppServiceBase()
        {
            LocalizationSourceName = MobileWebConsts.LocalizationSourceName;
        }
    }
}