using System;

namespace APP.Business.Services.Models
{
    public class ArticleListModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string SubTitle { get; set; }
        public string Summary { get; set; }
        public int Viewed { get; set; }
        public string Author { get; set; }
        public int CommentCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int State { get; set; }
        public FileModel File { get; set; }
    }
}
