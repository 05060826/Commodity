using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Commodity.Controllers
{
    public class AdminsInfoController : Controller
    {
        //管理员登录后台总页面
        public IActionResult AllShow()
        {
            return View();
        }

//---------------------------------------------------------------------------------------------
        /// <summary>
        /// 图书分类信息显示
        /// </summary>
        /// <returns></returns>
        public IActionResult BookTypeShow()
        {
            return View();
        }
        /// <summary>
        /// 图书分类添加
        /// </summary>
        /// <returns></returns>
        public IActionResult BookTypeAdd()
        {
            return View();
        }

        /// <summary>
        /// 图书次级分类添加
        /// </summary>
        /// <returns></returns>
        public IActionResult BookTypeAddNext()
        {
            return View();
        }

        /// <summary>
        /// 图书分类修改信息
        /// </summary>
        /// <returns></returns>
        public IActionResult BookTypeUpdate(string tid)
        {
            ViewBag.tid = tid;
            return View();
        }

//----------------------------------------------------------------------------------

        /// <summary>
        /// 图书信息显示
        /// </summary>
        /// <returns></returns>
        public IActionResult BookShow()
        {
            return View();
        }

        /// <summary>
        /// 图书信息添加
        /// </summary>
        /// <returns></returns>
        public IActionResult BookAdd()
        {
            return View();
        }

        /// <summary>
        /// 图书信息修改
        /// </summary>
        /// <returns></returns>
        public IActionResult BookUpdate()
        {
            return View();
        }



//---------------------------------------------------------------------------------------------
        /// <summary>
        /// 供应商信息管理
        /// </summary>
        /// <returns></returns>
        public IActionResult SupplierShow()
        {
            return View();
        }
        /// <summary>
        /// 供应商信息注册添加新的供应商
        /// </summary>
        /// <returns></returns>
        public IActionResult SupplierAdd()
        {
            return View();
        }
        /// <summary>
        /// 供应商信息修改
        /// </summary>
        /// <returns></returns>
        public IActionResult SupplierUpdate()
        {
            return View();
        }



//-----------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 顾客信息列表管理
        /// </summary>
        /// <returns></returns>
        public IActionResult ClientShow()
        {
            return View();
        }
        /// <summary>
        /// 顾客信息注册新的顾客信息
        /// </summary>
        /// <returns></returns>
        public IActionResult ClientAdd()
        {
            return View();
        }
        /// <summary>
        /// 顾客信息修改
        /// </summary>
        /// <returns></returns>
        public IActionResult ClientUpdate()
        {
            return View();
        }
    }
}
