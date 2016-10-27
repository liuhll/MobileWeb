using System;
using System.Collections.Generic;
using System.Timers;
using Abp.Logging;
using Jueci.MobileWeb.Common.Tools;

namespace Jueci.MobileWeb.Common.Caches.Impl
{
    public class CacheDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ICacheDictionary<TKey, TValue>
        where TValue : ICacheObject
    {
        private Timer _cacheTimer;
        private const int TimeInterval = 1000 * 10;


        public CacheDictionary()
        {
            if (IsAutoClearCache && CacheDuration > 0)
            {
                _cacheTimer = new Timer(TimeInterval);
                _cacheTimer.AutoReset = false;
                _cacheTimer.Elapsed += _cacheTimer_Elapsed;
                _cacheTimer.Start();
            }
        }

        private void _cacheTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (this)
                {
                    foreach (var item in this)
                    {
                        if (DateTime.Now >= item.Value.OperateTime.AddMinutes(CacheDuration))
                        {
                            this.Remove(item.Key);
                            LogHelper.Logger.Info(string.Format("缓存定时器将缓存计划库中key值为{0}的计划从中移除",item.Key));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Logger.Error("缓存定时器自动执行失败,异常" + ex.Message);
            }
            finally
            {
                _cacheTimer.Start();
            }
        }

        public bool IsAutoClearCache
        {
            get { return ConfigHelper.GetBoolValues("IsAutoClearCache"); }
        }

        //public DateTime OperateTime { get; }

        public int CacheDuration
        {
            get
            {
                if (IsAutoClearCache)
                {
                    return ConfigHelper.GetIntValues("CacheDuration");
                }
                return 0;
            }
        }
    }
}