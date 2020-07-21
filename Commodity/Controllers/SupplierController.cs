using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Commodity.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //图书
        public IActionResult BookOk()
        {
            return View();
        }
        public IActionResult BookRead()
        {
            return View();
        }
        public IActionResult BookCk()
        {
            return View();
        }
        //订单
        public IActionResult OrderOk()
        {
            return View();
        }
    }
}