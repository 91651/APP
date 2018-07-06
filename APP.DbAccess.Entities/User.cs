﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace APP.DbAccess.Entities
{
    public class User : User<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class User<TKey> : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        [MaxLength(32)]
        public override TKey Id { get; set; }

        [MaxLength(255)]
        public override string UserName { get; set; }

        [MaxLength(255)]
        public override string NormalizedUserName { get; set; }

        [MaxLength(255)]
        public override string Email { get; set; }

        [MaxLength(255)]
        public override string NormalizedEmail { get; set; }

        [MaxLength(255)]
        public override string PasswordHash { get; set; }

        [MaxLength(255)]
        public override string SecurityStamp { get; set; }

        [MaxLength(255)]
        public override string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(255)]
        public override string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Discriminator { get; set; }

        public virtual ICollection<Article> Articles { get; } = new List<Article>();
    }
}
