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
        public IActionResult ADD()
        {
            return View();
        }
        //结算
        public IActionResult Account(int rid)
        {
            ViewBag.rid = rid;
            return View();
        }
        
        //查询全部订单
        public IActionResult RecoundShowAll()
        {
            return View();
        }
        //查询 未付款订单
        public IActionResult Non_Payment()
        {
            return View();
        }
        //查询 待确认收货订单
        public IActionResult Affirm()
        {
            return View();
        }
        //查询 待评价订单
        public IActionResult Evaluate()
        {
            return View();
        }
        //空
        public IActionResult UpdReceipt()
        {
            return View();
        }
        


        //模型
        public IActionResult Model()
        {
            return View();
        }
        public IActionResult Model2()
        {
            return View();
        }
        public IActionResult Model3()
        {
            return View();
        }
        public IActionResult Model4()
        {
            return View();
        }
    }
}