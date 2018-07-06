using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using APP.DbAccess.Entities;

namespace APP.Framework.Identity
{
    public class RoleStore<TRole, TContext, TKey> : RoleStore<TRole, TContext, TKey, UserRole<TKey>, RoleClaim<TKey>>,
        IQueryableRoleStore<TRole>,
        IRoleClaimStore<TRole>
        where TRole : Role<TKey>
        where TKey : IEquatable<TKey>
        where TContext : DbContext
    {
        public RoleStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer) { }
        protected override RoleClaim<TKey> CreateRoleClaim(TRole role, Claim claim)
        {
            return new RoleClaim<TKey> { RoleId = role.Id, ClaimType = claim.Type, ClaimValue = claim.Value };
        }
    }
}
