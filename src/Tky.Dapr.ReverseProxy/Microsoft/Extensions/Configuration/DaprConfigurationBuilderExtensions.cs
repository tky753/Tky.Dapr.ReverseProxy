using System.Collections.Generic;
using Dapr;

namespace Microsoft.Extensions.Configuration
{
    public static class DaprConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDaprConfig(
            this IConfigurationBuilder configurationBuilder)
        {
            var httpEndpoint = DaprDefaults.GetDefaultHttpEndpoint();
            return configurationBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("Yarp:Clusters:dapr-sidecar:Destinations:d1:Address", httpEndpoint),
            });
        }
    }
}