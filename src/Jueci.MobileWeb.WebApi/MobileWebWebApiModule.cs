using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Jueci.MobileWeb
{
    [DependsOn(typeof(AbpWebApiModule), typeof(MobileWebApplicationModule))]
    public class MobileWebWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(MobileWebApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
