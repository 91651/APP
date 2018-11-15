using System.Text;
using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;
using APP.DbAccess.Repositories;
using APP.Framework.Identity;
using APP.Framework.Services;
using AutoMapper;
using BC.Microsoft.DependencyInjection.Plus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace APP.UI.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(option => option.UseSqlite(Configuration["ConnectionStrings:SqliteConnection"]));
            services.AddIdentity<User, Role>().AppAddEntityFrameworkStores<AppDbContext>();

            //添加AutoMapper服务
            services.AddAutoMapper();
            services.AddCors(options => { options.AddPolicy("AllowAllOrigin", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials(); }); });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:56833",
                    ValidAudience = "http://localhost:8010",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            //注入项目类
            services.AddScopedScan(typeof(Repository<>));
            services.AddScopedScan(typeof(Service));
            services.AddMvc(options =>
            {
                //默认不允许匿名访问
                options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build()));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseSwaggerUi3WithApiExplorer(s =>
            {
                s.GeneratorSettings.Title = "APP";
                s.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
                s.PostProcess = document =>
                {
                    document.Security.Add((new SwaggerSecurityRequirement { { "Bearer", new string[] { } }, }));
                };
                s.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = SwaggerSecurityApiKeyLocation.Header,
                }));
            });
            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "ClientApp";
            //    if (env.IsDevelopment())
            //    {
            //        //spa.UseProxyToSpaDevelopmentServer("http://localhost:8010");
            //        spa.UseVueDevelopmentServer(npmScript: "serve"); // 目前没有针对Vue的CliServer https://github.com/aspnet/JavaScriptServices/issues/1712
            //    }
            //});
        }
    }
}
