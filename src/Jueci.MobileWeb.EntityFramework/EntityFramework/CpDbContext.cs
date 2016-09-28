using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Abp.EntityFramework;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Mappings;

namespace Jueci.MobileWeb.EntityFramework
{
    public class CpDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        #region DbSet

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

   

        #endregion


        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public CpDbContext()
            : base("cpconstr")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MobileWebDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MobileWebDbContext since ABP automatically handles it.
         */
        public CpDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

         
        }
    }
}