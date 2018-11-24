using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pjax.AspNetCore.Mvc;

namespace APP.UI.MVC.Controllers
{
    public class TimelineController : Controller
    {
        [Pjax]
        public IActionResult Index()
        {
            return View();
        }
    }
}