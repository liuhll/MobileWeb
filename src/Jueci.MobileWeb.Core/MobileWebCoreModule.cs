using System.Reflection;
using Abp.Modules;

namespace Jueci.MobileWeb
{
    public class MobileWebCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
