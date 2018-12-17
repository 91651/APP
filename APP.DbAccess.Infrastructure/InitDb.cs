using System;
using System.Linq;
using APP.DbAccess.Entities;

namespace APP.DbAccess.Infrastructure
{
    public static class InitDb
    {
        public static void InitUser(this AppDbContext db)
        {
            //var db = (AppDbContext)service.GetService(typeof(AppDbContext));

            if (db.Database != null)
            {
                if (!db.Users.Any())
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        AccessFailedCount = 0,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Discriminator = "User",
                        Email = "mail@521.org.cn",
                        EmailConfirmed = false,
                        LockoutEnabled = true,
                        NormalizedUserName = "ADMIN",
                        NormalizedEmail = "MAIL@521.ORG.CN",
                        PasswordHash = "AQAAAAEAACcQAAAAEFRrMZEV8ngJ2MVoC7/+O3zqpOua+PLqLww/3jHMvMYA1Zkqu4D3Lh07L6wGghaTgQ==", //Qwer1234!
                        SecurityStamp = "e17d18ae-ee90-4612-bc96-d2a5d05b4a05",
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
