using System;
using System.Collections.Generic;

namespace Jueci.MobileWeb.Lottery.Models.Transfer
{
    public class NewLottery
    {
        public IList<int> LotteryResult { get; set; }

        public int NextPeriod { get; set; }

        public DateTime NextPeriodTimePoint { get; set; }

        public int CurrentPeriod { get; set; }

        public string CurrentPeriodDisplay
        {
            get { return string.Format("第{0}期开奖",CurrentPeriod); }
        }

        public int NextPeriodTime { get; set; }

        public string NextPeriodDisplay
        {
            get { return string.Format("第{0}期开奖倒计时", NextPeriod); }
        }

    }
}