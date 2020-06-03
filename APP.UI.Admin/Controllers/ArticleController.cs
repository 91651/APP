using System.Collections.Generic;
using APP.DbAccess.Entities;
using APP.Framework.IView;
using APP.Business.Services;
using APP.Business.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using System.Threading.Tasks;
using APP.Framework.DynamicLinq;

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

        [HttpPost]
        public Task<string> AddArticle(ArticleModel model)
        {
            model.UserId = _userManager.GetUserId(_signInManager.Context.User);
            model.OwnerId = _userManager.GetUserId(_signInManager.Context.User);
            return _articleService.AddArticleAsync(model);
        }

        [HttpDelete("{id}")]
        public Task<bool> DelArticle(string id)
        {
            return _articleService.DelArticleAsync(id);
        }

        [HttpPut("{id}")]
        public Task<bool> UpdateArticle(ArticleModel model)
        {
            return _articleService.UpdateArticleAsync(model);
        }

        [HttpGet("{id}")]
        public Task<ArticleModel> GetArticle(string id)
        {
            return _articleService.GetArticleAsync(id);
        }

        [HttpGet]
        public Task<PageResult<List<ArticleListModel>>> GetArticles([FromQuery]SearchArticleModel model)
        {
            return _articleService.GetArticlesAsync(model);
        }
        [HttpPost("/api/channel")]
        public Task<string> AddChannel(ChannelModel model)
        {
            return _articleService.AddChannelAsync(model);
        }
        [HttpGet("/api/channel/{id}")]
        public Task<List<Cascader>> GetChannelsToCascader(string id)
        {
            return _articleService.GetChannelsToCascaderAsync(id);
        }
    }
}
