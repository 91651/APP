using System;
using System.ComponentModel.DataAnnotations;
using APP.Framework.Util;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class Role : Role<string>
    {
        public Role()
        {
            Id = Guid.NewGuid().ToString(10);
        }
    }
    public class Role<TKey> : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        [MaxLength(40)]
        public override TKey Id { get; set; }

        [MaxLength(255)]
        public override string Name { get; set; }

        [MaxLength(255)]
        public override string NormalizedName { get; set; }

        [MaxLength(255)]
        public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString(10);

        [MaxLength(255)]
        public string Discriminator { get; set; }
    }
}
