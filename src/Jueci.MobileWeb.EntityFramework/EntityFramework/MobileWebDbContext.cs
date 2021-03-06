﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using Abp.EntityFramework;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Mappings;

namespace Jueci.MobileWeb.EntityFramework
{
    public class MobileWebDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        #region DbSet

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<UserPlanComputionInfo> UserPlanComputionInfos { get; set; }

        public virtual IDbSet<LotteryPlanLib> LotteryPlanLibs { get; set; }

        public virtual IDbSet<LotteryConfig> LotteryConfigs { get; set; }

        #endregion


        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public MobileWebDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in MobileWebDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of MobileWebDbContext since ABP automatically handles it.
         */
        public MobileWebDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserPlanComputionInfoMap());
            modelBuilder.Configurations.Add(new LotteryPlanLibMap());

            modelBuilder.Configurations.Add(new LotteryConfigMap());
        }
    }
}
