using System.Collections.Generic;
using Dapr;
using Tky.Dapr.ReverseProxy;

namespace Microsoft.Extensions.Configuration
{
    public static class DaprConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDaprConfig(
            this IConfigurationBuilder configurationBuilder)
        {
            var httpEndpoint = DaprDefaults.GetDefaultHttpEndpoint();
            return configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                [$"{DaprYarpConst.SectionKey}:Clusters:{DaprYarpConst.DaprSideCarCluster}:Destinations:d1:Address"] =
                    httpEndpoint,
                [$"{DaprYarpConst.SectionKey}:Routes:{DaprYarpConst.ApiRoute}:ClusterId"] = DaprYarpConst.DaprSideCarCluster,
                [$"{DaprYarpConst.SectionKey}:Routes:{DaprYarpConst.ApiRoute}:Match:Path"] = "api/{**catch-all}"
            });
        }
    }
}