using System.Data.Entity.ModelConfiguration;
using Jueci.MobileWeb.Lottery.Models;

namespace Jueci.MobileWeb.Mappings
{
    public class LotteryConfigMap : EntityTypeConfiguration<LotteryConfig>
    {
        public LotteryConfigMap()
        {
            ToTable("LotteryConfig");

            HasKey(t => t.CpType);

            Ignore(t => t.Id);
        }
    }
}
