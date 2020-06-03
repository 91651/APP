using System.Text;
using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;
using APP.DbAccess.Repositories;
using APP.Business.Services;
using AutoMapper;
using BC.Microsoft.DependencyInjection.Plus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APP.Business.Services.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;

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
                //�����û��������
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6
                };
            });
            services.AddControllersWithViews();
            services.AddOpenApiDocument();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddAutoMapper(typeof(Mappings));
            services.AddCors(options => { options.AddPolicy("AllowAllOrigin", builder => { builder.SetIsOriginAllowed(allowed => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials(); }); });

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
            //ע����Ŀ��
            services.AddScopedScan(typeof(Repository<>));
            services.AddScopedScan(typeof(Service));
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //������������ʹ��Swagger
                app.UseOpenApi(settings =>
                {
                    settings.PostProcess = (document, request) =>
                    {
                        document.Info.Title = "aa";
                    };
                });
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseInitDb();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors("AllowAllOrigin");
            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8010");
                    //�Զ���������Vue
                    //spa.UseVueDevelopmentServer(npmScript: "serve"); // Ŀǰû�����Vue��CliServer https://github.com/aspnet/JavaScriptServices/issues/1712
                }
            });
        }
    }
}
