using System;

namespace Jueci.MobileWeb.Common.Caches
{
    public interface ICacheObject
    {
        DateTime CacheDateTime { get; }

        DateTime OperateTime { get; set; }

    }
}