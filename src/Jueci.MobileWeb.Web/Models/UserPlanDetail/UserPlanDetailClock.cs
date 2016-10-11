using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Jueci.MobileWeb.Web.Models.UserPlanDetail
{
    public class UserPlanDetailClock
    {
        public UserPlanDetailClock(int tabIndex,Lottery.Models.Transfer.UserPlanDetail userPlanDetail)
        {
            TabIndex = tabIndex;
            UserPlanDetail = userPlanDetail;
        }

        public int TabIndex { get; set; }

        public Lottery.Models.Transfer.UserPlanDetail UserPlanDetail { get; set; }

    }
}