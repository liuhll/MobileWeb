using Abp.Domain.Entities;

namespace Jueci.MobileWeb.Lottery.Models
{
    public class LotteryConfig : Entity<string>
    {
        public string CpType { get; set; }

        public string ConfigData { get; set; }
    }
}