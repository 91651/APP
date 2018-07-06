using System;
using System.Linq;
using APP.DbAccess.Entities;

namespace APP.DbAccess.Infrastructure
{
    public static class InitDb
    {
        public static void InitUser(this IServiceProvider service)
        {
            var db = (AppDbContext)service.GetService(typeof(AppDbContext));

            if (db.Database != null)
            {
                if (!db.Users.Any())
                {
                    var user = new User
                    {
                        Id = "456d2b07-2ae6-430d-a0a1-4fec833387a1",
                        AccessFailedCount = 0,
                        ConcurrencyStamp = "9c8134b9-eed3-4c8b-90f8-372fb9b062fa",
                        Discriminator = "User",
                        Email = "admin@ceshi.app",
                        EmailConfirmed = false,
                        LockoutEnabled = true,
                        NormalizedUserName = "ADMIN",
                        NormalizedEmail = "ADMIN@CESHI.APP",
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
