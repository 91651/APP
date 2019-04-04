using System.Text;
using System.Threading.Tasks;
using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;
using APP.DbAccess.Repositories;
using APP.Business.Services;
using AutoMapper;
using BC.Microsoft.DependencyInjection.Plus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag.AspNetCore;
using AspNetCore.VueCli;

namespace APP.UI.Admin
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
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                //配置用户密码策略
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6
                };
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
            //添加AutoMapper服务
            services.AddAutoMapper();
            services.AddCors(options => { options.AddPolicy("AllowAllOrigin", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials(); }); });
            services.AddAuthentication().AddCookie().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("521.org.cn"))
                };
            });

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            //注入项目类
            services.AddScopedScan(typeof(Repository<>));
            services.AddScopedScan(typeof(Service));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //添加Swagger服务 *暂时不要升级新版本，由于.Net Core 2.2 bug，新版本会有415错误
            services.AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<AppDbContext>();
                db.Database.EnsureCreated();
                db.InitUser();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //开发环境可以使用Swagger
                app.UseSwagger(settings =>
                {
                    settings.PostProcess = (document, request) =>
                    {
                        document.Info.Title = "APP";
                    };
                });
                app.UseSwaggerUi3(settings => settings.DocumentPath = "{documentName}/swagger.json");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors("AllowAllOrigin");
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:8010");
                    //自动编译启动Vue
                    spa.UseVueDevelopmentServer(npmScript: "serve"); // 目前没有针对Vue的CliServer https://github.com/aspnet/JavaScriptServices/issues/1712
                }
            });
        }
    }
}
