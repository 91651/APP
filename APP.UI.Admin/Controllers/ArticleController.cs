using System.Collections.Generic;
using APP.Framework.Services;
using APP.Framework.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost, Route("AddArticle")]
        public ActionResult<string> AddArticle(ArticleModel model)
        {
            return _articleService.AddArticle(model);
        }

        [HttpGet, Route("GetArticles")]
        public ActionResult<List<ArticleListModel>> GetArticles()
        {
            return _articleService.GetArticles();
        }
    }
}
