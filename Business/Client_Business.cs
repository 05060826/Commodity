using DataAccess.DataDal;
using DataAccess.DataModels;
using System;
using System.Collections.Generic;

namespace Business
{
    //顾客业务处理
    public class Client_Business
    {
        EFHelper<OrderItems> Od = new EFHelper<OrderItems>();
        EFHelper<UserorderRecound> Ur = new EFHelper<UserorderRecound>();
        EFHelper<BankInfo> Ban = new EFHelper<BankInfo>();
        //显示出所有已经放入购物车中的商品
        public List<OrderItems>GetOrders()
        {
            var dt = Od.GetAll();
            return dt;
        }
        //添加订单
        public  int ADD(UserorderRecound ur)
        {
            var add = Ur.Add(ur);
            return add;
        }

        //删除订单
        public int Delete( OrderItems id)
        {
            var de = Od.Delete(id);
            return de;
        }
        //查询转态
        public List<UserorderRecound> GetUserorderRecound()
        {
            var data = Ur.GetAll();
            return data;
        }
        //显示付款账户
        public List<BankInfo>GetBanks()
        {
            var dt = Ban.GetAll();
            return dt;
        }

        //修改支付状态，收货，评价状态
        public  int Updata(UserorderRecound ur)
        {
            var up = Ur.Modify(ur);
            return up;
        }
        //修改银行卡余额
        public int UpBnak(BankInfo bank)
        {
            var up = Ban.Modify(bank);
            return up;
        }
    }
}
