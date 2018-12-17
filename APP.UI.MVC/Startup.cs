using System.IO;
using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;
using APP.DbAccess.Repositories;
using APP.Framework.Identity;
using APP.Framework.Services;
using AutoMapper;
using BC.Microsoft.DependencyInjection.Plus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace APP.UI.MVC
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
            services.AddDbContext<AppDbContext>(option => option.UseSqlite(Configuration["ConnectionStrings:SqliteConnection"]));
            services.AddIdentity<User, Role>().AppAddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddScopedScan(typeof(Repository<>));
            services.AddScopedScan(typeof(Service));
            services.AddAutoMapper();
            services.AddMvc(options => { options.RespectBrowserAcceptHeader = true; })
            .AddXmlSerializerFormatters()
            .AddRazorOptions(a => { a.AreaViewLocationFormats.Add("~/{2}/Views/{1}/{0}.cshtml"); a.AreaViewLocationFormats.Add("~/{2}/Views/Shared/{0}.cshtml"); })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //重定向文件路径
            //app.UseRewriter(new RewriteOptions() /*{ StaticFileProvider = Environment.ContentRootFileProvider }*/.AddRewrite("^static/(.*)", $"{Configuration["AppSettings:UploadPath"]}/$1", true));
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = "/static",
                FileProvider = new PhysicalFileProvider(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), Configuration["AppSettings:UploadPath"])))
            }
                );
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}");
                routes.MapRoute(name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
