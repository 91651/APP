using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using APP.DbAccess.Entities;


namespace APP.Framework.Identity
{
    public class UserStore<TUser, TRole, TContext, TKey> : UserStore<TUser, TRole, TContext, TKey, UserClaim<TKey>, UserRole<TKey>, UserLogin<TKey>, UserToken<TKey>, RoleClaim<TKey>>
        where TUser : User<TKey>
        where TRole : Role<TKey>
        where TContext : DbContext
        where TKey : IEquatable<TKey>
    {

        public UserStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }

        protected override UserRole<TKey> CreateUserRole(TUser user, TRole role)
        {
            return new UserRole<TKey>()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override UserClaim<TKey> CreateUserClaim(TUser user, Claim claim)
        {
            var userClaim = new UserClaim<TKey> { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);
            return userClaim;
        }

        protected override UserLogin<TKey> CreateUserLogin(TUser user, UserLoginInfo login)
        {
            return new UserLogin<TKey>
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        protected override UserToken<TKey> CreateUserToken(TUser user, string loginProvider, string name, string value)
        {
            return new UserToken<TKey>
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }
    }
}
