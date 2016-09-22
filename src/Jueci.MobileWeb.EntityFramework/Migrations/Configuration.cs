using System.Data.Entity.Migrations;

namespace Jueci.MobileWeb.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MobileWeb.EntityFramework.MobileWebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MobileWeb";
        }

        protected override void Seed(MobileWeb.EntityFramework.MobileWebDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
