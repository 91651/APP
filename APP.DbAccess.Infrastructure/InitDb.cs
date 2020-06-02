using System;
using System.Linq;
using APP.DbAccess.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace APP.DbAccess.Infrastructure
{
    public static class InitDb
    {
        public static void UseInitDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (db.Database.EnsureCreated())
                {
                    db.InitUser();
                }
            }
        }
        public static void InitUser(this AppDbContext db)
        {
            if (db.Database != null)
            {
                if (!db.Users.Any())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        AccessFailedCount = 0,
                        ConcurrencyStamp = Guid.NewGuid().ToString("N"),
                        Discriminator = "User",
                        Email = "mail@521.org.cn",
                        EmailConfirmed = false,
                        LockoutEnabled = true,
                        NormalizedUserName = "ADMIN",
                        NormalizedEmail = "MAIL@521.ORG.CN",
                        PasswordHash = "AQAAAAEAACcQAAAAEAnWnclF0/4mxVdi5gDAs0oASFMDFlb+RyPIjsBtb/ZbyrgVwRwZxXJ0CM8+6r8iHg==", //admin
                        SecurityStamp = "577ZGQ626XNGP53HYRDHXC4OVYNPQW5Q",
                        TwoFactorEnabled = false,
                        UserName = "admin"
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }
    }
}
