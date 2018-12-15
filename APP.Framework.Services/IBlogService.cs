using System.Collections.Generic;
using APP.Framework.Services.Models;
using IView.AspNetCore.DynamicLinq;

namespace APP.Framework.Services
{
    public interface IBlogService
    {
        ArticleModel GetArticle(string Id);
        ArticleModel GetPrevArticle(string channelId, string id);
        ArticleModel GetNextArticle(string channelId, string id);
        List<ChannelModel> GetChannels(string pid = null);
        int UpdateArticleViewed(string id);
    }
}