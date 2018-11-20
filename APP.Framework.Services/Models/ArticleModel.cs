using System;

namespace APP.Framework.Services.Models
{
    public class ArticleModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string UserId { get; set; }
        public string OwnerId { get; set; }
        public string[] ChannelId { get; set; }
        public string Author { get; set; }
        public int Editor { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int? State { get; set; }
    }
}
