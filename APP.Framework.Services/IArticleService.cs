using System.Collections.Generic;
using APP.Framework.Services.Models;

namespace APP.Framework.Services
{
    public interface IArticleService
    {
        string AddArticle(ArticleModel model);
        List<ArticleListModel> GetArticles();
    }
}