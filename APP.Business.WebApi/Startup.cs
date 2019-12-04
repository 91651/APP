
using APP.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.AspNetCore;

namespace APP.Business.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApiDocument();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBusinessException();
                app.UseDeveloperExceptionPage();
                
                //app.UseBusinessException(new CustomExceptionMiddleWareOption(
                //     handleType: CustomExceptionHandleType.Both,  //根据url关键字决定处理方式
                //     jsonHandleUrlKeys: new PathString[] { "/api" },
                //     errorHandingPath: "/home/error"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            

            app.UseOpenApi(settings =>
            {
                settings.PostProcess = (document, request) =>
                {
                    document.Info.Title = Configuration["Project:Name"];
                };
            });
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
