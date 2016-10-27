using System;

namespace Jueci.MobileWeb.Common.Tools
{
    public static class ConvertHelper
    {
        public static T StringToEnum<T>(string values)
        {
            return (T) Enum.Parse(typeof(T), values.ToLower());
        }
    }
}