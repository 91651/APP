using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class RoleClaim : RoleClaim<string>
    {
        public RoleClaim()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class RoleClaim<TKey> : IdentityRoleClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        [MaxLength(32)]
        public TKey Id { get; set; }

        [MaxLength(32)]
        public override TKey RoleId { get; set; }

        [MaxLength(10)]
        public override string ClaimType { get; set; }

        [MaxLength(255)]
        public override string ClaimValue { get; set; }

        [ForeignKey("RoleId")]
        public Role<TKey> Role { get; set; }
    }
}

