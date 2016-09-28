using System;
using System.Collections.Generic;
using System.Linq;
using Camew;
using Camew.Extend;
using Camew.Lottery;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Lottery.Event;

namespace Jueci.MobileWeb.Lottery.Service
{
    public class LotteryServiceInfo
    {
        private LotteryEngine _lotteryEngine;
        private UpdateLotteryData _updateLotteryData;

        public LotteryEngine LotteryEngine
        {
            get { return _lotteryEngine; }
        }

        public UpdateLotteryData UpdateLotteryData
        {
            get { return _updateLotteryData; }
        }

        public LotteryServiceInfo(LotteryEngine lotteryEngine)
        {
            _lotteryEngine = lotteryEngine;
            _updateLotteryData = new UpdateLotteryData(lotteryEngine);
            
        }

        
    }
}