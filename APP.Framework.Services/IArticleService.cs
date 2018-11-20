using System.Collections.Generic;
using APP.Framework.Services.Models;
using IView.AspNetCore.DynamicLinq;

namespace APP.Framework.Services
{
    public interface IArticleService
    {
        ResultModel<string> AddArticle(ArticleModel model);
        ResultModel DelArticle(string Id);
        ResultModel<ArticleModel> GetArticle(string Id);
        ResultModel<List<ArticleListModel>> GetArticles(SearchArticleModel model);
        ResultModel<string> AddChannel(ChannelModel model);
        List<Cascader> GetChannelsToCascader(string channelId);
    }
}