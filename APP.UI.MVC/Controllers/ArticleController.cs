using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Pjax, HttpGet("/article/{id}")]
        public IActionResult Index(string id)
        {
            var article = _blogService.GetArticle(id);
            ViewBag.Article = article;
            var channelId = article.ChannelId.Last();
            ViewBag.PrevArticle = _blogService.GetPrevArticle(id, channelId);
            ViewBag.NextArticle = _blogService.GetNextArticle(id, channelId);
            return View();
        }

        [Pjax, HttpGet("/channel/{id}")]
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