using System;
using System.ComponentModel.DataAnnotations;

namespace APP.DbAccess.Entities
{
    public class Comment
    {
        [Key]
        [MaxLength(40)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(40)]
        public string ParentId { get; set; }
        [MaxLength(40)]
        public string OwnerId { get; set; }
        [MaxLength(60)]
        public string Author { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength]
        public string Content { get; set; }
    }
}
