using System;
using Abp;

namespace Jueci.MobileWeb.Common
{
    public class JueciException : AbpException
    {
        public JueciException(string msg) : base(msg)
        {
        }

        public JueciException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}