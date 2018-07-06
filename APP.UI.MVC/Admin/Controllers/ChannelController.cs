using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.UI.MVC.Admin.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APP.UI.MVC.Admin.Controllers
{
    [Area("Admin")]
    public class ChannelController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
