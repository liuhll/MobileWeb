using System;
using System.Configuration;
using Castle.Core.Internal;

namespace Jueci.MobileWeb.Common.Tools
{
    public class ConfigHelper
    {
        public static bool GetBoolValues(string key)
        {
            var value = GetValuesByKey(key);
            return Convert.ToBoolean(value);
        }

        public static int GetIntValues(string key)
        {
            var value = GetValuesByKey(key);
            return Convert.ToInt32(value);
        }

        public static string GetValuesByKey(string key)
        {
            string values = ConfigurationManager.AppSettings[key];
            if ( values == null)
            {
                throw new Exception(string.Format("应用程序中没有key为{0}的设置", key));
            }
            return values;
        }
    }
}