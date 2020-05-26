using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APP.Framework.Util;

namespace APP.DbAccess.Entities
{
    [Table("1421asfasd")]
    public class Article
    {
        [Key]
        [MaxLength(40)]
        public string Id { get; set; } = Guid.NewGuid().ToString(10);

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string SubTitle { get; set; }
        [MaxLength(500)]
        public string Summary { get; set; }
        [MaxLength(40)]
        public string UserId { get; set; }

        [MaxLength(40)]
        public string OwnerId { get; set; }

        [MaxLength(40)]
        public string ChannelId { get; set; }

        [MaxLength(60)]
        public string Author { get; set; }
        public int? Editor { get; set; }
        [MaxLength]
        public string Content { get; set; }
        [MaxLength]
        public string MdContent { get; set; }
        [DefaultValue(0)]
        public int Viewed { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int State { get; set; }

        [ForeignKey("UserId")]
        public User<string> User { get; set; }

        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ICollection<File> Files { get; } = new List<File>();
    }
}
