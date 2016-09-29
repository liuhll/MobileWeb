using Camew.Lottery.AppService;
using Jueci.MobileWeb.Common.Enums;

namespace Jueci.MobileWeb.Lottery.Models.Transfer
{
    public class PlanDetailList
    {
        //064-066期|1 2 5 7|2|065|22049|对,
        public string CycleName { get; set; }

        public string CurrentCycleName { get; set; }

        public DMSMType DsType { get; set; }

        public string GuessValue { get; set; }

        public string LotteryResult { get; set; }

        public int EndIndex { get; set; }

        public string RightOrWrong { get; set; }



    }
}