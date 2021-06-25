using System;
using System.Threading.Tasks;
using Yarp.ReverseProxy.Abstractions.Config;

namespace Tky.Dapr.ReverseProxy
{
    public class DaprTransformProvider : ITransformProvider
    {
        public void ValidateRoute(TransformRouteValidationContext context)
        {
        }

        public void ValidateCluster(TransformClusterValidationContext context)
        {
        }

        public void Apply(TransformBuilderContext context)
        {
            if(context.Route.RouteId == DaprYarpConst.ApiRoute)
            {
                context.AddRequestTransform(transformContext =>
                {
                    var index = transformContext.Path.Value!.IndexOf('/', 5); // format: /api/xxxx
                    var appId = transformContext.Path.Value.Substring(5, index - 5);
                    var newPath = transformContext.Path.Value.Substring(index);
                    transformContext.ProxyRequest.RequestUri = new Uri($"{transformContext.DestinationPrefix}/v1.0/invoke/{appId}/method{newPath}");
                    return ValueTask.CompletedTask;
                });
            }
            
        }
    }
}