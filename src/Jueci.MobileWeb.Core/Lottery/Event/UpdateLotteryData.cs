using System;
using System.Timers;
using Abp.Logging;
using Camew.Lottery.AppService;
using Castle.Core.Internal;
using Jueci.MobileWeb.Lottery.Event.EventData;

namespace Jueci.MobileWeb.Lottery.Event
{
    public class UpdateLotteryData
    {
        public event EventHandler<UpdateLotteyDataEventArgs> UpdateLotteryEventHandler;

        private readonly object lockObj = new object();

        private LotteryEngine _lotteryEngine;

        private int Time_Interval = 5000;

        private Timer _timer;

        public UpdateLotteryData(LotteryEngine lotteryEngine)
        {           
            _lotteryEngine = lotteryEngine;
            _timer = new Timer(Time_Interval);
            _timer.AutoReset = false;
            _timer.Elapsed += _moniteTimer_Elapsed;
            _timer.Start();

        }

        private void _moniteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (lockObj)
                {
                    //定时触发事件
                    OnEventHandler();
                }
                

            }
            catch (Exception ex)
            {
               LogHelper.Logger.Error("定时任务异常"+ex.Message);
            }
            finally
            {
                _timer.Start();
            }
        }

        internal void CallUpdateLotterEventHandler()
        {
            OnEventHandler();
        }

        protected void OnEventHandler()
        {
            UpdateLotteryEventHandler?.Invoke(this, new UpdateLotteyDataEventArgs(_lotteryEngine));
        }
    }
}