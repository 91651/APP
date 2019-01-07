using System.Collections.Generic;
using APP.Framework.IView;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface IArticleService
    {
        ResultModel<string> AddArticle(ArticleModel model);
        ResultModel DelArticle(string id);
        ResultModel<ArticleModel> GetArticle(string id);
        ResultModel UpdateArticle(ArticleModel model);
        ResultModel<List<ArticleListModel>> GetArticles(SearchArticleModel model);
        ResultModel<string> AddChannel(ChannelModel model);
        List<Cascader> GetChannelsToCascader(string channelId);
    }
}