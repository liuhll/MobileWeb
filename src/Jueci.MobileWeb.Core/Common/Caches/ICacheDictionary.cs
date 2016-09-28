using System;
using System.Collections.Generic;
using Abp.Runtime.Caching;

namespace Jueci.MobileWeb.Common.Caches
{
    public interface ICacheDictionary<TKey,TValue> : IDictionary<TKey, TValue>
        where TValue : ICacheObject
    {
        bool IsAutoClearCache { get; }

        //DateTime OperateTime { get; }

        int CacheDuration { get; }
    }
}