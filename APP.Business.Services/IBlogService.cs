using System.Collections.Generic;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface IBlogService
    {
        ArticleModel GetArticle(string Id);
        ArticleModel GetPrevArticle(string channelId, string id);
        ArticleModel GetNextArticle(string channelId, string id);
        ChannelModel GetChannel(string id);
        List<ChannelModel> GetChannels(string pid = null);
        int UpdateArticleViewed(string id);
    }
}