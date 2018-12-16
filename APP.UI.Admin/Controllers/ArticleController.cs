using System.Collections.Generic;
using APP.DbAccess.Entities;
using APP.Framework.Services;
using APP.Framework.Services.Models;
using IView.AspNetCore.DynamicLinq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;

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
        [return: NotNull]
        public ActionResult<ResultModel<string>> AddArticle(ArticleModel model)
        {
            model.UserId = _userManager.GetUserId(_signInManager.Context.User);
            model.OwnerId = _userManager.GetUserId(_signInManager.Context.User);
            return _articleService.AddArticle(model);
        }

        [HttpPost, Route("DelArticle")]
        [return: NotNull]
        public ActionResult<ResultModel> DelArticle(string Id)
        {
            return _articleService.DelArticle(Id);
        }

        [HttpPost, Route("UpdateArticle")]
        [return: NotNull]
        public ActionResult<ResultModel> UpdateArticle(ArticleModel model)
        {
            return _articleService.UpdateArticle(model);
        }

        [HttpPost, Route("GetArticle")]
        [return: NotNull]
        public ActionResult<ResultModel<ArticleModel>> GetArticle(string Id)
        {
            return _articleService.GetArticle(Id);
        }

        [HttpPost, Route("GetArticles")]
        [return: NotNull]
        public ActionResult<ResultModel<List<ArticleListModel>>> GetArticles(SearchArticleModel model)
        {
            return _articleService.GetArticles(model);
        }
        [HttpPost, Route("AddChannel")]
        [return: NotNull]
        public ActionResult<ResultModel<string>> AddChannel(ChannelModel model)
        {
            return _articleService.AddChannel(model);
        }
        [HttpGet, Route("GetChannelsToCascader")]
        [return: NotNull]
        public ActionResult<List<Cascader>> GetChannelsToCascader(string channelId)
        {
            return _articleService.GetChannelsToCascader(channelId);
        }
    }
}
