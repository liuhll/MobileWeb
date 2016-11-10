using Jueci.MobileWeb.Common.Enums;

namespace Jueci.MobileWeb.Lottery.Models
{
    public class LotteryPlanLib : PlanComputionBase
    {
        public string VCode { get; set; }

        public PlanLibState State { get; set; }

        public bool IsNeedAccessRight
        {
            get { return !string.IsNullOrEmpty(VCode); }
        }

        public string TeamName { get; set; }

        public string Contact { get; set; }


    }
}