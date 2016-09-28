using System;
using Camew.Lottery.AppService;

namespace Jueci.MobileWeb.Lottery.Event.EventData
{
    public class UpdateLotteyDataEventArgs : EventArgs
    {

        private LotteryEngine _lotteryEngine;

        public LotteryEngine LotteryEngine
        {
            get { return _lotteryEngine; }
        }

        public UpdateLotteyDataEventArgs(LotteryEngine lotteryEngine)
        {
            _lotteryEngine = lotteryEngine;
        }
    }
}