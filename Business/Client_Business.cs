using DataAccess.DataDal;
using DataAccess.DataModels;
using System;
using System.Collections.Generic;

namespace Business
{
    //顾客业务处理
    public class Client_Business
    {
        EFHelper<OrderItems> cus = new EFHelper<OrderItems>();

        //显示出所有已经放入购物车中的商品
        public List<OrderItems>GetOrders()
        {
            var dt = cus.GetAll();
            return dt;
        }
    }
}
