using APP.Business.Services;
using APP.Business.Services.Models;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using System.Collections.Generic;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : Controller
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpPost, Route("GetMenus")]
        public ActionResult<List<MenuModel>> GetMenus()
        {
            return _commonService.GetMenus();
        }
    }
}
