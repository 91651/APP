using System;
using System.Linq;
using APP.DbAccess.Entities;
using APP.Framework.Util;

namespace APP.DbAccess.Infrastructure
{
    public static class InitDb
    {
        public static void Init(this AppDbContext db)
        {
            InitUser(db);
            InitMenu(db);
        }
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
        public static void InitMenu(this AppDbContext db)
        {
            if (db.Database != null)
            {
                if (!db.Menus.Any())
                {
                    var menu = new Menu
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "home",
                        Title = "首页",
                        Icon="home",
                        Path = "/",
                        Order = 1,
                        State = 1

                    };
                    db.Menus.Add(menu);
                    db.SaveChanges();
                }
            }
        }
    }
}
