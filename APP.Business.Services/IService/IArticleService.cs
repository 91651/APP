using System.Collections.Generic;
using APP.Framework.IView;
using APP.Business.Services.Models;
using System.Threading.Tasks;
using APP.Framework.DynamicLinq;

namespace APP.Business.Services
{
    public interface IArticleService
    {
        Task<string> AddArticleAsync(ArticleModel model);
        Task<bool> DelArticleAsync(string id);
        Task<ArticleModel> GetArticleAsync(string id);
        Task<bool> UpdateArticleAsync(ArticleModel model);
        Task<PageResult<List<ArticleListModel>>> GetArticlesAsync(SearchArticleModel model);
        Task<string> AddChannelAsync(ChannelModel model);
        Task<List<Cascader>> GetChannelsToCascaderAsync(string channelId);
    }
}