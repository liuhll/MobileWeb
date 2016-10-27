using System;
using Abp.Domain.Entities;

namespace Jueci.MobileWeb.Lottery.Models
{
    public class PlanComputionBase : Entity<string>
    {
        public int UId { get; set; }

        public int SId { get; set; }

        public string PlanComputionInfo { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}