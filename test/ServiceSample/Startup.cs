using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ServiceSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMvcCore().AddApiExplorer().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                if(Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") != null)
                    c.DocumentFilter<SwaggerDaprPrefixFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ServiceSample", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger(c =>
            {
                // c.PreSerializeFilters.Add((swagger, httpReq) =>
                // {
                //     swagger.Servers.Clear();
                //     swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://localhost:44363/api/service-sample" } };
                // });
            });
            app.UseSwaggerUI(c =>
            {
                // c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("v1/swagger.json", "ServiceSample v1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Map("/", c =>
                {
                    c.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
            });
        }
    }
}