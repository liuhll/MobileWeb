namespace Jueci.MobileWeb.Lottery.Models
{
    public class LotteryPlanLib : PlanComputionBase
    {
        public string VCode { get; set; }

        public int State { get; set; }

        public bool IsNeedAccessRight
        {
            get { return !string.IsNullOrEmpty(VCode); }
        }
    }
}