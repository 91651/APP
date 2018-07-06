using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.DbAccess.Entities
{
    public class Article
    {
        [Key]
        [MaxLength(32)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string SubTitle { get; set; }

        [MaxLength(32)]
        public string UserId { get; set; }

        [MaxLength(32)]
        public string OwnerId { get; set; }

        [MaxLength(32)]
        public string ChannelId { get; set; }

        [MaxLength(60)]
        public string Author { get; set; }

        [MaxLength]
        public string Context { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int State { get; set; }

        [ForeignKey("UserId")]
        public User<string> User { get; set; }

        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }
    }
}
