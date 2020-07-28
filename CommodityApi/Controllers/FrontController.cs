using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataDal;
using DataAccess.DataModels;
using DataAccess.Login;
using java.sql;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CommodityApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class FrontController : ControllerBase
    {

        [Route("login")]
        [HttpPost]
        public LoginClass Login(LoginClass login)
        {
            CommercedataContext context = new CommercedataContext();
            LoginClass ret = new LoginClass();
            JWTHelper jwtHeader = new JWTHelper();


            string jiami = "";
            List<Customer> client = new List<Customer>();
            List<UserRoderInfo> supplier = new List<UserRoderInfo>();
            List<ManageInfo> Administrator = new List<ManageInfo>();


            if (login.Table == "Customer")
            {
                client = context.Customer.ToList();
                if (!string.IsNullOrEmpty(login.AccountName))
                {
                    client = client.Where(s => s.AccountName.Equals(login.AccountName.Trim())).ToList();
                }

                if (!string.IsNullOrEmpty(login.AccountPwd))
                {
                    client = client.Where(s => s.AccountPwd.Equals(login.AccountPwd.Trim())).ToList();
                }
                if (client.Count == 1)
                {
                    Dictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("AccountName", client[0].AccountName);
                    keys.Add("TouMoney", client[0].AccountPwd);
                    keys.Add("AccountId", client[0].AccountId);

                    jiami = jwtHeader.GetToken(keys, 600000);
                    ret.AccountName = login.AccountName;
                    ret.AccountPwd = login.AccountPwd;
                    ret.Quan = "顾客";
                    ret.Encipherment = jiami;
                    ret.Name = client[0].UserName;
                }
                return ret;
            }

            if (login.Table == "UserRoderInfo")
            {
                supplier = context.UserRoderInfo.ToList();
                if (!string.IsNullOrEmpty(login.AccountName))
                {
                    supplier = supplier.Where(s => s.ContactName.Equals(login.AccountName.Trim())).ToList();
                }

                if (!string.IsNullOrEmpty(login.AccountPwd))
                {
                    supplier = supplier.Where(s => s.SupplierPwd.Equals(login.AccountPwd.Trim())).ToList();
                }
                if (supplier.Count == 1)
                {
                    Dictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ContactName", supplier[0].ContactName);
                    keys.Add("SupplierPwd", supplier[0].SupplierPwd);
                    keys.Add("SuppLierID", supplier[0].SuppLierId);

                    jiami = jwtHeader.GetToken(keys, 600000);
                    ret.AccountName = login.AccountName;
                    ret.AccountPwd = login.AccountPwd;
                    ret.Quan = "供应商";

                    ret.Encipherment = jiami;
                    ret.Name = supplier[0].TrueName;
                }
                return ret;
            }
            if (login.Table == "ManageInfo")
            {
                Administrator = context.ManageInfo.ToList();
                if (!string.IsNullOrEmpty(login.AccountName))
                {
                    Administrator = Administrator.Where(s => s.ManagerUserName.Equals(login.AccountName.Trim())).ToList();
                }

                if (!string.IsNullOrEmpty(login.AccountPwd))
                {
                    Administrator = Administrator.Where(s => s.ManagerPwd.Equals(login.AccountPwd.Trim())).ToList();
                }
                if (Administrator.Count == 1)
                {
                    Dictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ManagerUserName", Administrator[0].ManagerUserName);
                    keys.Add("ManagerPwd", Administrator[0].ManagerPwd);
                    keys.Add("ManageId", Administrator[0].ManageId);

                    jiami = jwtHeader.GetToken(keys, 600000);
                    ret.AccountName = login.AccountName;
                    ret.AccountPwd = login.AccountPwd;
                    ret.Quan = "管理员";
                    ret.Encipherment = jiami;
                    ret.Name = Administrator[0].ManagerUserName;
                }
                return ret;
            }
            else
            {
                return ret;
            }

         
        }

        [Route("show")]
        [HttpGet]
        public string Show(string type,string actor, string bookname)
        {
            CommercedataContext context = new CommercedataContext();


            var list = (from f in context.SupplierBookInfo
                        join
                        s in context.BookInfo on f.Isbn equals s.Isbn
                        join
                        t in context.NextClassType on s.NclassId equals t.NclassId
                        join
                        fs in context.AuthorInfo on s.AuthorId equals fs.AuthorId
                        select new { bookName = s.Title, actoreName = fs.Aname, chuBan = s.Publish, time =Convert.ToDateTime(s.PublishTime), price = s.Price, zhe = f.Discount,shangJia=f.SupplierId,bookBian=f.Isbn,bookType=t.NclassName }).ToList();

            if (type != null)
            {
                list=list.Where(s => s.bookType.Equals(type)).ToList();
            }
            if (actor != null)
            {
                list=list.Where(s => s.actoreName.Equals(actor)).ToList();
            }
            if (bookname != null)
            {
                list=list.Where(s => s.bookName.Equals(bookname)).ToList();
            }
            string str = "";
            Dictionary<string, object> obje = new Dictionary<string, object>();
            obje.Add("data", list);
            obje.Add("count", list.Count);
            str = JsonConvert.SerializeObject(obje);

            return str;
        }

        [HttpPost]
        [Route("addOrder")]
        public int AddOrder(OrderItems addOrder) {

            int count = 0;
            CommercedataContext context = new CommercedataContext();
            OrderItems item = new OrderItems();

            item.BookName = addOrder.BookName;
            item.BookPrice = addOrder.BookPrice;
            item.Isbn = addOrder.Isbn;
            item.OrderId = addOrder.OrderId;
            item.OrderitemId = addOrder.OrderitemId;
            item.Quantity = 1;
            item.Statue = "未支付";
            item.SupplierId = addOrder.SupplierId;

            List<OrderItems> list = context.OrderItems.ToList();

            list=list.Where(s => s.OrderitemId.Equals(addOrder.OrderitemId) && s.OrderId.Equals(addOrder.OrderId)).ToList();
            if (list.Count != 0)
            {
                return 0;
            }
            context.OrderItems.Add(item);
            count = context.SaveChanges();
            
            return count;

        }

        //添加顾客
        [HttpPost]
        [Route("addGu")]
        public int AddGu(Customer customer)
        {

            int count = 0;
            CommercedataContext context = new CommercedataContext();
            
            customer.UserReddate = DateTime.Now;
            List<Customer> list = context.Customer.ToList();

            list = list.Where(s => s.AccountId.Equals(customer.AccountId)).ToList();
            if (list.Count != 0)
            {
                return 0;
            }
            context.Customer.Add(customer);
            count = context.SaveChanges();

            return count;

        }

        //添加供应商
        [HttpPost]
        [Route("addGong")]
        public int AddGong(UserRoderInfo userRoder)
        {

            int count = 0;
            CommercedataContext context = new CommercedataContext();

            userRoder.RegDate = DateTime.Now;
            List<UserRoderInfo> list = context.UserRoderInfo.ToList();

            list = list.Where(s => s.SuppLierId.Equals(userRoder.SuppLierId)).ToList();
            if (list.Count != 0)
            {
                return 0;
            }
            context.UserRoderInfo.Add(userRoder);
            count = context.SaveChanges();

            return count;

        }

        //添加管理员
        [HttpPost]
        [Route("addGuan")]
        public int AddGuan(ManageInfo manageInfo)
        {

            int count = 0;
            CommercedataContext context = new CommercedataContext();

            List<ManageInfo> list = context.ManageInfo.ToList();

            list = list.Where(s => s.ManageId.Equals(manageInfo.ManageId)).ToList();
            if (list.Count != 0)
            {
                return 0;
            }
            context.ManageInfo.Add(manageInfo);
            count = context.SaveChanges();

            return count;

        }
    }
}