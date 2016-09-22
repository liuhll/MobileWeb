using System.Reflection;
using Abp.Modules;

namespace Jueci.MobileWeb
{
    [DependsOn(typeof(MobileWebCoreModule))]
    public class MobileWebApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
