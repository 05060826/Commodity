using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
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
        //可以查询全部订单，未付款订单，待确认收货订单和待评价订单等订单信息
        public Client_ShowModel ClientShow(int pageIndex = 1, int pageSize = 3)
        {
            var list = _business.GetOrders();
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
        public string NonPayment(int pageName = 0, int limitName = 0)
        {
            var list = _business.GetUserorderRecound();
            var Wlist = list.Where(m => m.PayStatues.Equals("未付款")).ToList();
            List<UserorderRecound> slist = Wlist.Skip((pageName - 1) * limitName).Take(limitName).ToList();
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("code", 0);
            obj.Add("msg", "");
            obj.Add("count", Wlist.Count);
            obj.Add("data", slist);
            return JsonConvert.SerializeObject(obj);
        }


        [HttpGet]
        //查询 待确认收货订单
        public string Affirm(int pageName = 0, int limitName = 0)
        {
            var list = _business.GetUserorderRecound();
            var Wlist = list.Where(m => m.DelivaeryStatue.Equals("待确认收货")).ToList();
            List<UserorderRecound> slist = Wlist.Skip((pageName - 1) * limitName).Take(limitName).ToList();
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("code", 0);
            obj.Add("msg", "");
            obj.Add("count", Wlist.Count);
            obj.Add("data", slist);
            return JsonConvert.SerializeObject(obj);
        }

        [HttpGet]
        //查询 待评价订单
        public string Evaluate(int pageName = 0, int limitName = 0)
        {
            var list = _business.GetUserorderRecound();
           
            var Wlist = list.Where(m => m.ReplayStatue.Equals("待评价")).ToList();

            List<UserorderRecound> slist = Wlist.Skip((pageName - 1) * limitName).Take(limitName).ToList();
            Dictionary<string, object> obj = new Dictionary<string, object>();
            obj.Add("code", 0);
            obj.Add("msg", "");
            obj.Add("count", Wlist.Count);
            obj.Add("data", slist);
            return JsonConvert.SerializeObject(obj);
        }
    }



}