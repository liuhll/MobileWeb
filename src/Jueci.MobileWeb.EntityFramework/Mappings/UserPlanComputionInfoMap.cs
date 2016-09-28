using System.Data.Entity.ModelConfiguration;
using Jueci.MobileWeb.Lottery.Models;

namespace Jueci.MobileWeb.Mappings
{
    public class UserPlanComputionInfoMap : EntityTypeConfiguration<UserPlanComputionInfo>
    {
        public UserPlanComputionInfoMap()
        {
            ToTable("UserPlanComputionInfo");

            HasKey(t=>new { t.UId,t.SId});

            Ignore(t => t.Id);
        }
    }
}