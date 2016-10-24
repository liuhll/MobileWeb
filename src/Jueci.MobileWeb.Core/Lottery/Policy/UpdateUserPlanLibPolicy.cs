using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeuci.SalesSystem.Helper;
using Jueci.MobileWeb.Common.Tools;
using Jueci.MobileWeb.Lottery.Models;

namespace Jueci.MobileWeb.Lottery.Policy
{
    public class UpdateUserPlanLibPolicy
    {
        private readonly PlanCacheArgs _planCacheArgs;


        public UpdateUserPlanLibPolicy(PlanCacheArgs planCacheArgs)
        {
            _planCacheArgs = planCacheArgs;
            
        }

        public bool IsValidTime()
        {
            var expirationDuration = ConfigHelper.GetIntValues("ExpirationDuration");
            var now = DateTime.Now;
            var requestTime = DateTimeHelper.UnixTimestampToDateTime(_planCacheArgs.Timestamp);
            if (requestTime.AddMinutes(expirationDuration) < now 
                || requestTime.AddMinutes(-expirationDuration) > now)
            {
                return false;
            }
            return true;
        }

        public bool IsLegalSign()
        {
            var saltKey = ConfigHelper.GetValuesByKey("SecretKey");
            var keytStr = string.Format("{0}{1}{2}{3}", _planCacheArgs.Uid,_planCacheArgs.Sid,_planCacheArgs.Timestamp, saltKey);
            return EncryptionHelper.EncryptSHA256(keytStr).Equals(_planCacheArgs.Sign);
        }
    }
}
