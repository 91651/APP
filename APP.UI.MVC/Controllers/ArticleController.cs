using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.Framework.Services;
using Microsoft.AspNetCore.Mvc;
using Pjax.AspNetCore.Mvc;

namespace APP.UI.MVC.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [Pjax, HttpGet("/article/{id}")]
        public IActionResult Index(string Id)
        {
            var article = _articleService.GetArticle(Id).Data;
            ViewBag.Article = article;
            var channelId = article.ChannelId.Last();
            ViewBag.PrevArticle = _articleService.GetPrevArticle(Id, channelId).Data;
            ViewBag.NextArticle = _articleService.GetNextArticle(Id, channelId).Data;
            return View();
        }
    }
}