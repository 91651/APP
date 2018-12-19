using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APP.Framework.Util;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class UserLogin : UserLogin<string>
    {
        public UserLogin()
        {
            Id = Guid.NewGuid().ToString(10);
        }
    }
    public class UserLogin<TKey> : IdentityUserLogin<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        [MaxLength(40)]
        public TKey Id { get; set; }

        [MaxLength(40)]
        public override TKey UserId { get; set; }

        [MaxLength(255)]
        public override string LoginProvider { get; set; }

        [MaxLength(255)]
        public override string ProviderKey { get; set; }

        [ForeignKey("UserId")]
        public User<TKey> User { get; set; }
    }
}
