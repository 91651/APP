using System.Collections.Generic;
using APP.Framework.Services.Models;
using IView.AspNetCore.DynamicLinq;

namespace APP.Framework.Services
{
    public interface IArticleService
    {
        string AddArticle(ArticleModel model);
        ResultModel<List<ArticleListModel>> GetArticles(SearchArticleModel model);
        string AddChannel(ChannelModel model);
        List<Cascader> GetChannelsToCascader(string channelId);
    }
}