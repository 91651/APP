﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using APP.DbAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP.DbAccess.Infrastructure
{
    public class AppDbContext : AppDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseLoggerFactory(LoggerFactory);
        }
    }
    public class AppDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey, UserClaim<TKey>, UserRole<TKey>, UserLogin<TKey>, RoleClaim<TKey>, UserToken<TKey>>
        where TUser : User<TKey>
        where TRole : Role<TKey>
        where TKey : IEquatable<TKey>
    {
        public AppDbContext(DbContextOptions options) : base(options)
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

    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null && entity.ClrType.CustomAttributes.All(a => a.AttributeType != typeof(TableAttribute)))
                {
                    entity.SetTableName(entity.GetDefaultTableName());
                }
            }

        }
    }
}
