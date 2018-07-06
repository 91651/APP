using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class UserClaim : UserClaim<string>
    {
        public UserClaim()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class UserClaim<TKey> : IdentityUserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        [MaxLength(32)]
        public TKey Id { get; set; }

        [MaxLength(32)]
        public override TKey UserId { get; set; }

        [MaxLength(10)]
        public override string ClaimType { get; set; }

        [MaxLength(255)]
        public override string ClaimValue { get; set; }

        [ForeignKey("UserId")]
        public User<TKey> User { get; set; }
    }
}
