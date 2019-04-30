using System.Linq;
using APP.Business.Services;
using APP.Business.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Pjax.AspNetCore.Mvc;

namespace APP.UI.MVC.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IBlogService _blogService;

        public ArticleController(IArticleService articleService, IBlogService blogService)
        {
            _articleService = articleService;
            _blogService = blogService;
        }

        [Pjax, HttpGet(@"/article/{id:regex(^\w{{10}}$)}"), HttpGet(@"/a/{id:regex(^\w{{10}}$)}"), HttpGet(@"/p/{id:regex(^\w{{10}}$)}")]
        public IActionResult Index(string id)
        {
            var article = _blogService.GetArticle(id);
            ViewBag.Article = article;
            var channelId = article.ChannelId.Last();
            ViewBag.PrevArticle = _blogService.GetPrevArticle(id, channelId);
            ViewBag.NextArticle = _blogService.GetNextArticle(id, channelId);
            _blogService.UpdateArticleViewed(id);
            return View();
        }

        [Pjax, HttpGet(@"/channel/{id=null}/{page=1}"), HttpGet(@"/c/{id=null}/{page=1}")]
        public IActionResult Channel(string id, int page)
        {
            var search = new SearchArticleModel();
            if (!string.IsNullOrWhiteSpace(id))
            {
                search.ChannelId = id;
                ViewBag.Channel = _blogService.GetChannel(id);
            }
            search.Take = 20;
            search.Skip = search.Take * (page - 1);
            var articles = _articleService.GetArticles(search).Data;
            ViewBag.Articles = articles;
            return View();
        }
    }
}