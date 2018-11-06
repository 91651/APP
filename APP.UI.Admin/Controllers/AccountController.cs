using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.UI.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            var list = new List<string>();
            list.Add("����");
            list.Add("����");
            list.Add("����");
            return list;
        }
    }
}
