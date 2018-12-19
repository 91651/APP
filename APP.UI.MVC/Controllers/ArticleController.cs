using System.Linq;
using APP.Framework.Services;
using APP.Framework.Services.Models;
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

        [Pjax, HttpGet("/article/{id}"), HttpGet("/a/{id}"), HttpGet("/p/{id}")]
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

        [Pjax, HttpGet("/channel/{id}"), HttpGet("/c/{id}")]
        public IActionResult Channel(string id)
        {
            var search = new SearchArticleModel();
            search.Take = 20;
            var articles = _articleService.GetArticles(search).Data;
            ViewBag.Articles = articles;
            return View();
        }
    }
}