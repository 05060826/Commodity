using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public Client_ShowModel ClientShow(int pageIndex = 1, int pageSize = 3)
        {
            return null;
        }

        [HttpGet]
        //可以查询全部订单，未付款订单，待确认收货订单和待评价订单等订单信息
        public string RecoundShow( int pageName = 0, int limitName = 0)
        {
            //var list = _Business.CheckShowModel();
            //List<ShowModel> slist = list.Skip((pageName - 1) * limitName).Take(limitName).ToList();
            //Dictionary<string, object> obj = new Dictionary<string, object>();
            //obj.Add("code", 0);
            //obj.Add("msg", "");
            //obj.Add("count", list.Count);
            //obj.Add("data", slist);
            //return JsonConvert.SerializeObject(obj);

            return null;

        }
    }



}