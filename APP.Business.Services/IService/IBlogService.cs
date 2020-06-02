using System.Collections.Generic;
using System.Threading.Tasks;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface IBlogService
    {
        Task<ArticleModel> GetArticleAsync(string Id);
        Task<ArticleModel> GetPrevArticleAsync(string channelId, string id);
        Task<ArticleModel> GetNextArticleAsync(string channelId, string id);
        Task<ChannelModel> GetChannelAsync(string id);
        Task<List<ChannelModel>> GetChannelsAsync(string pid = null);
        Task<int> UpdateArticleViewedAsync(string id);
    }
}