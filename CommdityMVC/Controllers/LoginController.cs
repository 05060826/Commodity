
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Commodity.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ZhuCeGu()
        {
            return View();
        }
        public IActionResult ZhuCeGong()
        {
            return View();
        }
        public IActionResult ZhuCeGuan()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Show(string type=null)
        {
            ViewBag.type = type;
            return View();
        }

    }
}