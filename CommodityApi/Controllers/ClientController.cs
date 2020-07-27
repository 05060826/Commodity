using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using com.sun.org.apache.xpath.@internal.operations;
using DataAccess.DataModels;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CommodityApi.Controllers
{
    /// <summary>
    /// 顾客，订单API控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        Client_Business _business = null;

        public ClientController()
        {
            _business = new Client_Business();
        }

        //获取所有已经放入购物车中的商品
        [HttpGet]
        public Client_ShowModel ClientShowOne(int pageIndex = 1, int pageSize = 3)
        {
            var list = _business.GetOrders().ToList();
            //总条数
            var totalCount = list.Count();
            //总页数
            var totalPage = totalCount / pageSize + (totalCount % pageSize > 0 ? 1 : 0);

            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            Client_ShowModel pageShowlist = new Client_ShowModel();
            pageShowlist.ShowList = list;
            pageShowlist.ToTalCount = totalCount;
            pageShowlist.PageTotal = totalPage;
            return pageShowlist;
        }
        //查询ID
        [HttpGet]
        public OrderItems GetId(string id)
        {
            var list = _business.GetOrders();
            OrderItems items = new OrderItems();
            items = list.Where(m => m.OrderId.Contains(id)).FirstOrDefault();
            return items;
        }
        [HttpPost]
        //添加订单
        public int ADD(UserorderRecound ur)
        {
            var len = _business.ADD(ur);
            return len;
        }
        [HttpGet]
        //删除订单
        public int Delete(string id)
        {
            OrderItems items = new OrderItems();
            var list = _business.GetOrders();
            items = list.Where(m => m.OrderitemId.Contains(id)).FirstOrDefault();
            var de = _business.Delete(items);
            return de;
        }
        //修改转态
        [HttpPost]
        public int UpPa(UserorderRecound ur)
        {

            var PS = _business.Updata(ur);
            return PS;
        }
        //修改
        public int XiuGai(string id, string Zd)
        {
            UserorderRecound use = new UserorderRecound();
            var list = _business.GetUserorderRecound();
            use = list.Where(m => m.OrderId.Contains(id)).FirstOrDefault();
            int n;
            use.OrderStatue = Zd;
            n = _business.Updata(use);
            return n;
        }

        //修改付款转态
        [HttpPost]
        public int UpPa(string ur)
        {
            var list = _business.GetUserorderRecound();
            var id = list.Where(m => m.PayStatues.Contains(ur)).FirstOrDefault();
            var PS = _business.Updata(id);
            return PS;
        }

        //修改发货转态
        [HttpPost]
        public int UpDs(string ur)
        {
            var list = _business.GetUserorderRecound();
            var id = list.Where(m => m.DelivaeryStatue.Contains(ur)).FirstOrDefault();
            var PS = _business.Updata(id);
            return PS;
        }

        //修改回复货转态
        [HttpPost]
        public int Uprs(string ur)
        {
            var list = _business.GetUserorderRecound();
            var id = list.Where(m => m.ReplayStatue.Contains(ur)).FirstOrDefault();
            var PS = _business.Updata(id);
            return PS;
        }
        [HttpGet]
        //可以查询全部订单，未付款订单，待确认收货订单和待评价订单等订单信息
        public string RecoundShowAll(int pageName = 0, int limitName = 0)
        {
            var list = _business.GetUserorderRecound();
            List<UserorderRecound> slist = list.Skip((pageName - 1) * limitName).Take(limitName).ToList();
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("code", 0);
            obj.Add("msg", "");
            obj.Add("count", list.Count);
            obj.Add("data", slist);
            return JsonConvert.SerializeObject(obj);
        }
        
        [HttpGet]
        //查询 未付款订单
        public UserShowModel NonPaymentOne(int pageIndex = 1, int pageSize = 3)
        {
            var list = _business.GetUserorderRecound();
            var Wlist = list.Where(m => m.PayStatues.Equals("未支付")).ToList();
            //总条数
            var totalCount = Wlist.Count();
            //总页数
            var totalPage = totalCount / pageSize + (totalCount % pageSize > 0 ? 1 : 0);


            Wlist = Wlist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            UserShowModel pageShowlist = new UserShowModel();
            pageShowlist.ShowList = Wlist;
            pageShowlist.ToTalCount = totalCount;
            pageShowlist.PageTotal = totalPage;
            return pageShowlist;
        }



        [HttpGet]
        //查询 待确认收货订单
        public UserShowModel Affirm(int pageIndex = 1, int pageSize = 3)
        {
            var list = _business.GetUserorderRecound();

            var Wlist = list.Where(m => m.DelivaeryStatue.Contains("待确认收货")).ToList();

            //总条数
            var totalCount = Wlist.Count();
            //总页数
            var totalPage = totalCount / pageSize + (totalCount % pageSize > 0 ? 1 : 0);


            Wlist = Wlist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            UserShowModel pageShowlist = new UserShowModel();
            pageShowlist.ShowList = Wlist;
            pageShowlist.ToTalCount = totalCount;
            pageShowlist.PageTotal = totalPage;
            return pageShowlist;
        }

        [HttpGet]
        //查询 待评价订单
        public UserShowModel Evaluate(int pageIndex = 1, int pageSize = 3)
        {
            var list = _business.GetUserorderRecound();


            var Wlist = list.Where(m => m.ReplayStatue.Contains("待评价")).ToList();

            //总条数
            var totalCount = Wlist.Count();
            //总页数
            var totalPage = totalCount / pageSize + (totalCount % pageSize > 0 ? 1 : 0);


            Wlist = Wlist.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            UserShowModel pageShowlist = new UserShowModel();
            pageShowlist.ShowList = Wlist;
            pageShowlist.ToTalCount = totalCount;
            pageShowlist.PageTotal = totalPage;
            return pageShowlist;
        }
        //修改银行卡余额
        [HttpPost]
        public int UpBnak(BankInfo bank)
        {
            var count = _business.UpBnak(bank);
            return count;
        }
      
    }
}



