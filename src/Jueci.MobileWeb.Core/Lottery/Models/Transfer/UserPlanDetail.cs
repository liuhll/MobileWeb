using System.Collections.Generic;

namespace Jueci.MobileWeb.Lottery.Models.Transfer
{
    public class UserPlanDetail
    {

        public string PlanName { get; set; }

        public float RightRate { get; set; }

        public int MaxAlwaysRight { get; set; }

        public int MaxAlwaysWrong { get; set; }

        public int CurrentROrW { get; set; }

        public IList<int> RightTimes { get; set; }

        public IList<PlanDetailList> PlanDetails { get; set; }
    }
}