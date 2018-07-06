using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using APP.DbAccess.Entities;


namespace APP.DbAccess.Infrastructure
{
    public class AppDbContext : AppDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Channel> Channels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Channel>().ToTable("Channel"); ;
            modelBuilder.Entity<Article>().ToTable("Article"); ;
        }

    }
    public class AppDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey, UserClaim<TKey>, UserRole<TKey>, UserLogin<TKey>, RoleClaim<TKey>, UserToken<TKey>>
        where TUser : User<TKey>
        where TRole : Role<TKey>
        where TKey : IEquatable<TKey>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        protected AppDbContext()
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //基类OnModelCreating方法需要先执行
            //base.OnModelCreating(modelBuilder); //不使用默认表结构
            // Identity 中定义的实体
            modelBuilder.Entity<User<TKey>>().ToTable("User");//必须指定表名，才能改变数据库中对应表名
            modelBuilder.Entity<Role<TKey>>().ToTable("Role");
            modelBuilder.Entity<UserClaim<TKey>>().ToTable("UserClaim");
            modelBuilder.Entity<UserRole<TKey>>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin<TKey>>().ToTable("UserLogin");
            modelBuilder.Entity<RoleClaim<TKey>>().ToTable("RoleClaim");
            modelBuilder.Entity<UserToken<TKey>>().ToTable("UserToken");
        }
    }
}
