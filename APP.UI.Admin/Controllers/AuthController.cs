using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APP.DbAccess.Entities;
using APP.Framework.Services.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APP.UI.Admin.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<AuthModel>> SignIn(string name, string pwd)
        {
            var result = new AuthModel();
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                result.Status = "用户不存在！";
                return result;
            }
            var signIn = await _signInManager.PasswordSignInAsync(user, pwd, false, false);
            if (signIn.Succeeded)
            {
                var userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:56833",
                    audience: "http://localhost:8010",
                    claims: userPrincipal.Claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signinCredentials
                );
                result.UserName = user.UserName;
                result.Status = "成功";
                result.TokenType = JwtBearerDefaults.AuthenticationScheme;
                result.Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            }
            else if (signIn.IsLockedOut)
            {
                result.Status = "用户已经被锁定，目前无法登录！";
            }
            else
            {
                result.Status = "账号或密码无效！";
            }
            //return Unauthorized();
            return result;
        }
    }
}
