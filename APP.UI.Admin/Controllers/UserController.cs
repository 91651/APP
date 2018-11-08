using System.Collections.Generic;
using APP.Framework.Services;
using APP.Framework.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserModel>> Get()
        {
            return _userService.GetUsers();
        }
    }
}
