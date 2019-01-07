using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.Business.Services;
using APP.Business.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Pjax.AspNetCore.Mvc;

namespace APP.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IBlogService _blogService;

        public HomeController(IArticleService articleService, IBlogService blogService)
        {
            _articleService = articleService;
            _blogService = blogService;
        }

        [Pjax]
        public IActionResult Index()
        {
            var search = new SearchArticleModel();
            search.Take = 20;
            var articles = _articleService.GetArticles(search).Data;
            ViewBag.Articles = articles;
            return View();
        }
        public ActionResult RightSide()
        {
            ViewBag.Channels = _blogService.GetChannels();
            return View();
        }
    }
}
