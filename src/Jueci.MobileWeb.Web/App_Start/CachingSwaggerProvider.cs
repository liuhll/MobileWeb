using System.Collections.Generic;
using Swashbuckle.Swagger;

namespace Jueci.MobileWeb.Web
{
    internal class CachingSwaggerProvider : ISwaggerProvider
    {
        private ISwaggerProvider defaultProvider;

        public CachingSwaggerProvider(ISwaggerProvider defaultProvider)
        {
            this.defaultProvider = defaultProvider;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var sd = defaultProvider.GetSwagger(rootUrl, apiVersion);
 
            var rmPaths = new string[]
            {
                "/api/AbpCache/Clear", "/api/AbpCache/ClearAll",
                "/api/AbpServiceProxies","/api/services/app/lotteryPlan/GetUserPlanInfos",
                "/api/ServiceProxies"
            };
            foreach (var rmpath in rmPaths)
            {
                if (sd.paths.ContainsKey(rmpath))
                {
                    sd.paths.Remove(rmpath);
                }

            }

            return sd;
        }
    }
}