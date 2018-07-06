using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pjax.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using APP.DbAccess.Infrastructure;
using APP.DbAccess.Entities;
using APP.UI.MVC.Admin.Models;

namespace APP.UI.MVC.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(AppDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                @ViewData["Msg"] = "用户或密码不符合规范！";
                return View(user);
            }
            var result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, user.Remember, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            if (result.IsLockedOut)
            {
                @ViewData["Msg"] = "用户已经被锁定，目前无法登录！";
            }
            else
            {
                @ViewData["Msg"] = "账号或密码无效！";
            }
            return View(user);
        }
    }
}