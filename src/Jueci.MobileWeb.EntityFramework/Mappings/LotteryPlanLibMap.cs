using Jueci.MobileWeb.Lottery.Models;
using System.Data.Entity.ModelConfiguration;

namespace Jueci.MobileWeb.Mappings
{
    public class LotteryPlanLibMap : EntityTypeConfiguration<LotteryPlanLib>
    {
        public LotteryPlanLibMap()
        {
            ToTable("LotteryPlanLib");
            HasKey(t => t.Id);
        }

    }
}