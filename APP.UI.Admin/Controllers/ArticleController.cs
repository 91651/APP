using System.Collections.Generic;
using APP.DbAccess.Entities;
using APP.Framework.Services;
using APP.Framework.Services.Models;
using IView.AspNetCore.DynamicLinq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IArticleService _articleService;

        public ArticleController(UserManager<User> userManager, SignInManager<User> signInManager, IArticleService articleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _articleService = articleService;
        }

        [HttpPost, Route("AddArticle")]
        public ActionResult<ResultModel<string>> AddArticle(ArticleModel model)
        {
            model.UserId = _userManager.GetUserId(_signInManager.Context.User);
            model.OwnerId = _userManager.GetUserId(_signInManager.Context.User);
            return _articleService.AddArticle(model);
        }

        [HttpPost, Route("GetArticles")]
        public ActionResult<ResultModel<List<ArticleListModel>>> GetArticles(SearchArticleModel model)
        {
            return _articleService.GetArticles(model);
        }
        [HttpPost, Route("AddChannel")]
        public ActionResult<ResultModel<string>> AddChannel(ChannelModel model)
        {
            return _articleService.AddChannel(model);
        }
        [HttpGet, Route("GetChannelsToCascader")]
        public ActionResult<List<Cascader>> GetChannelsToCascader(string channelId)
        {
            return _articleService.GetChannelsToCascader(channelId);
        }
    }
}
