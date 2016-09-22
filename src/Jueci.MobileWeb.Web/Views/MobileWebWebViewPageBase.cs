using Abp.Web.Mvc.Views;

namespace Jueci.MobileWeb.Web.Views
{
    public abstract class MobileWebWebViewPageBase : MobileWebWebViewPageBase<dynamic>
    {

    }

    public abstract class MobileWebWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected MobileWebWebViewPageBase()
        {
            LocalizationSourceName = MobileWebConsts.LocalizationSourceName;
        }
    }
}