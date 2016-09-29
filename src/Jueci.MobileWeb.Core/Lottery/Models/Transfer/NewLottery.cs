using System;
using System.Collections.Generic;

namespace Jueci.MobileWeb.Lottery.Models.Transfer
{
    public class NewLottery
    {
        public string LotteryResult { get; set; }

        public int NextPeriod { get; set; }

        public DateTime NextPeriodTimePoint { get; set; }

        public int CurrentPeriod { get; set; }

        public int NextPeriodTime { get; set; }

    }
}