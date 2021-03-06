﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APP.Framework.Util;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class UserToken : UserToken<string>
    {
        public UserToken()
        {
            Id = Guid.NewGuid().ToString(10);
        }
    }
    public class UserToken<TKey> : IdentityUserToken<TKey>
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
        public override string Name { get; set; }

        [MaxLength(255)]
        public override string Value { get; set; }

        [ForeignKey("UserId")]
        public User<TKey> User { get; set; }
    }
}
