using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServiceSample
{
    public class SwaggerDaprPrefixFilter : IDocumentFilter
    {
        private readonly IConfiguration _configuration;

        public SwaggerDaprPrefixFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var appId = _configuration["AppId"];
            if(string.IsNullOrEmpty(appId))
                return;
            var paths = swaggerDoc.Paths;
            foreach (var pair in paths.ToList())
            {
                paths.Remove(pair.Key);
                paths.Add("/api/" + appId + pair.Key, pair.Value);
            }
        }
    }
}