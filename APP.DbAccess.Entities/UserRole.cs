using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APP.Framework.Util;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class UserRole : UserRole<string>
    {
        public UserRole()
        {
            Id = Guid.NewGuid().ToString(10);
        }
    }
    public class UserRole<TKey> : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        [MaxLength(40)]
        public TKey Id { get; set; }

        [MaxLength(40)]
        public override TKey UserId { get; set; }

        [ForeignKey("UserId")]
        public User<TKey> User { get; set; }

        [MaxLength(40)]
        public override TKey RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role<TKey> Role { get; set; }
    }
}
