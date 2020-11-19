
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using APP.Business.Services;
using APP.Business.Services.AutoMapper;
using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;
using APP.DbAccess.Repositories;
using AutoMapper;
using BC.Microsoft.DependencyInjection.Plus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace APP.UI.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            services.AddHeadElementHelper();
            services.AddAutoMapper(c =>
            {
                c.AddProfile(new Mappings());
            });
            services.AddAntDesign();
            services.AddDbContext<AppDbContext>(option => option.UseSqlite(Configuration["ConnectionStrings:SqliteConnection"]));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
            services.AddScopedScan(typeof(Repository<>));
            services.AddScopedScan(typeof(Service));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseInitDb();

            app.UseHttpsRedirection();

            app.UseHeadElementServerPrerendering();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions { RequestPath = "/static", FileProvider = new PhysicalFileProvider(Directory.CreateDirectory(Configuration["AppSettings:StaticContentPath"]).FullName) });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
