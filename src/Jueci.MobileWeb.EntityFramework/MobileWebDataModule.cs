using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using Jueci.MobileWeb.EntityFramework;

namespace Jueci.MobileWeb
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(MobileWebCoreModule))]
    public class MobileWebDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<MobileWebDbContext>(null);
        }
    }
}
