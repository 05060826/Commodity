using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Commodity.Controllers
{
    /// <summary>
    ///顾客，订单 MVC控制器 
    /// </summary>
    public class Client_Controller : Controller
    {
        //获取所有已经放入购物车中的商品
        public IActionResult Index()
        {
            return View();
        }
    }
}