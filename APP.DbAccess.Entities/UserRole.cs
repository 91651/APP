using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class UserRole : UserRole<string>
    {
        public UserRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class UserRole<TKey> : IdentityUserRole<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        [MaxLength(32)]
        public TKey Id { get; set; }

        [MaxLength(32)]
        public override TKey UserId { get; set; }

        [ForeignKey("UserId")]
        public User<TKey> User { get; set; }

        [MaxLength(32)]
        public override TKey RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role<TKey> Role { get; set; }
    }
}
