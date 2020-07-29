using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataDal;
using DataAccess.DataModels;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace CommodityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        //可售图书
        [HttpGet]
        public string GetBookInfo(string typeId, string authorName = "", string title = "", string pubish = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from da in context.BookInfo
                            join du in context.AuthorInfo
                            on da.AuthorId equals du.AuthorId
                            join dd in context.NextClassType
                            on da.NclassId equals dd.NclassId
                            where dd.NclassId == typeId
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, da.Price, du.AuthorId, du.Aname, dd.NclassId, dd.NclassName }).ToList();
                if (!string.IsNullOrEmpty(authorName))
                {
                    list = list.Where(n => n.Aname.Contains(authorName)).ToList();
                }
                if (!string.IsNullOrEmpty(title))
                {
                    list = list.Where(n => n.Title.Contains(title)).ToList();
                }
                if (!string.IsNullOrEmpty(pubish))
                {
                    list = list.Where(n => n.Publish.Contains(pubish)).ToList();
                }
                var slist = list.Skip((pageName - 1) * limitName).Take(limitName).ToList();
                Dictionary<string, object> obj = new Dictionary<string, object>();

                //前台通过key值获得对应的value值
                obj.Add("code", 0);
                obj.Add("msg", "");
                obj.Add("count", list.Count);
                obj.Add("data", slist);
                return JsonConvert.SerializeObject(obj);
            }
        }

        //分类
        [HttpGet]
        public List<NextClassType> GetTypeData()
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                List<NextClassType> list = context.NextClassType.ToList();
                return list;
            }
        }
        //根据编号查询图书详情
        [HttpGet]
        public string GetBookData(string Isbn)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from da in context.BookInfo
                            join du in context.AuthorInfo
                            on da.AuthorId equals du.AuthorId
                            join dd in context.NextClassType
                            on da.NclassId equals dd.NclassId
                            where da.Isbn == Isbn
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, da.Price, da.Revision, da.Page, du.AuthorId, du.Aname, dd.NclassId, dd.NclassName }).FirstOrDefault();
                return JsonConvert.SerializeObject(list);
            }
        }
        //添加到仓库
        [HttpPost]
        public int InsertBook(Dictionary<string, object> dic)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                SupplierBookInfo info = JsonConvert.DeserializeObject<SupplierBookInfo>(dic["obj"].ToString());
                string token = dic["token"].ToString();
                if (token == "undefined")
                {
                    return 0;
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return 0;
                }

                string SupplierId = ue.SuppLierId;
                var list = (from s in context.SupplierBookInfo where s.Isbn == info.Isbn select s).FirstOrDefault();
                if (list == null)
                {
                    info.Score = 0;
                    info.SaledQuantity = 0;
                    info.BookStues = "未出售";
                    info.SupplierId = SupplierId;
                    context.SupplierBookInfo.Add(info);
                    return context.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
        }
        //获取仓库未出售图书
        [HttpGet]
        public string GetCkBookInfo(string Token = "", string authorName = "", string title = "", string pubish = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (Token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(Token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;
                var list = (from da in context.BookInfo
                            join du in context.AuthorInfo
                            on da.AuthorId equals du.AuthorId
                            join dd in context.SupplierBookInfo
                            on da.Isbn equals dd.Isbn
                            where dd.BookStues == "未出售" && dd.SupplierId == SupplierId
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, dd.Price, dd.TotalQuantity, dd.Discount, dd.BookType, dd.SaledQuantity, dd.BookStues, du.AuthorId, du.Aname }).ToList();
                if (!string.IsNullOrEmpty(authorName))
                {
                    list = list.Where(n => n.Aname.Contains(authorName)).ToList();
                }
                if (!string.IsNullOrEmpty(title))
                {
                    list = list.Where(n => n.Title.Contains(title)).ToList();
                }
                if (!string.IsNullOrEmpty(pubish))
                {
                    list = list.Where(n => n.Publish.Contains(pubish)).ToList();
                }
                var slist = list.Skip((pageName - 1) * limitName).Take(limitName).ToList();
                Dictionary<string, object> obj = new Dictionary<string, object>();

                //前台通过key值获得对应的value值
                obj.Add("code", 0);
                obj.Add("msg", "");
                obj.Add("count", list.Count);
                obj.Add("data", slist);
                return JsonConvert.SerializeObject(obj);
            }
        }
        //根据编号查询图书详情
        [HttpGet]
        public string GetCkBookData(string Isbn)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from da in context.BookInfo
                            join du in context.AuthorInfo
                            on da.AuthorId equals du.AuthorId
                            join dc in context.NextClassType
                            on da.NclassId equals dc.NclassId
                            join dd in context.SupplierBookInfo
                             on da.Isbn equals dd.Isbn
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, dd.Price, dd.TotalQuantity, dd.Discount, dd.BookType, dd.SaledQuantity, dd.BookStues, du.AuthorId, du.Aname, dc.NclassName }).FirstOrDefault();
                return JsonConvert.SerializeObject(list);
            }
        }
        //图书上架
        [HttpGet]
        public int UpdaBookSate(string Isbn)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from s in context.SupplierBookInfo where s.Isbn == Isbn select s).FirstOrDefault();
                list.BookStues = "出售中";
                context.SupplierBookInfo.Update(list);
                return context.SaveChanges();
            }
        }
        //删除仓库图书
        [HttpGet]
        public int DelBookData(string Isbn)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from s in context.SupplierBookInfo where s.Isbn == Isbn select s).FirstOrDefault();
                context.SupplierBookInfo.Remove(list);
                return context.SaveChanges();
            }
        }
        //更新仓库
        [HttpPost]
        public int UpdBook(Dictionary<string, object> dic)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                SupplierBookInfo info = JsonConvert.DeserializeObject<SupplierBookInfo>(dic["obj"].ToString());
                string token = dic["token"].ToString();
                if (token == "undefined")
                {
                    return 0;
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return 0;
                }

                string SupplierId = ue.SuppLierId;
                var list = (from s in context.SupplierBookInfo where s.Isbn == info.Isbn select s).FirstOrDefault();
                list.Price = info.Price;
                list.Discount = info.Discount;
                list.TotalQuantity = info.TotalQuantity;
                list.BookType = info.BookType;
                context.SupplierBookInfo.Update(list);
                return context.SaveChanges();
            }
        }
        //获取仓库出售中图书
        [HttpGet]
        public string GetCkZBookInfo(string token = "", string authorName = "", string title = "", string pubish = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;
                var list = (from da in context.BookInfo
                            join du in context.AuthorInfo
                            on da.AuthorId equals du.AuthorId
                            join dd in context.SupplierBookInfo
                            on da.Isbn equals dd.Isbn
                            where dd.BookStues == "出售中" && dd.SupplierId == SupplierId
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, dd.Price, dd.TotalQuantity, dd.Discount, dd.BookType, dd.SaledQuantity, dd.BookStues, du.AuthorId, du.Aname }).ToList();
                if (!string.IsNullOrEmpty(authorName))
                {
                    list = list.Where(n => n.Aname.Contains(authorName)).ToList();
                }
                if (!string.IsNullOrEmpty(title))
                {
                    list = list.Where(n => n.Title.Contains(title)).ToList();
                }
                if (!string.IsNullOrEmpty(pubish))
                {
                    list = list.Where(n => n.Publish.Contains(pubish)).ToList();
                }
                var slist = list.Skip((pageName - 1) * limitName).Take(limitName).ToList();
                Dictionary<string, object> obj = new Dictionary<string, object>();

                //前台通过key值获得对应的value值
                obj.Add("code", 0);
                obj.Add("msg", "");
                obj.Add("count", list.Count);
                obj.Add("data", slist);
                return JsonConvert.SerializeObject(obj);
            }
        }
        //图书下架
        [HttpGet]
        public int UpdxBookSate(string token, string Isbn)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return 0;
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return 0;
                }

                string SupplierId = ue.SuppLierId;
                var list = (from s in context.SupplierBookInfo where s.Isbn == Isbn select s).FirstOrDefault();
                list.BookStues = "未出售";
                context.SupplierBookInfo.Update(list);
                return context.SaveChanges();
            }
        }
        //已卖出商品
        [HttpGet]
        public string GetGoodMC(string token = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;
                var list = (from da in context.OrderItems
                            join du in context.UserorderRecound
                            on da.OrderId equals du.OrderId
                            join dd in context.SupplierBookInfo
                            on da.SupplierId equals dd.SupplierId
                            where da.SupplierId == SupplierId
                            select new { da.Isbn, da.SupplierId, da.BookName, da.BookPrice, da.Quantity, da.Statue, du.OrderId, du.BuyNum, du.ConsigName, du.ClinchTime }).ToList();
                var slist = list.Skip((pageName - 1) * limitName).Take(limitName).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("data", slist);
                dic.Add("count", list.Count);
                return JsonConvert.SerializeObject(dic);
            }
        }

        //待付款商品
        [HttpGet]
        public string GetGoodDF(string token = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;
                var list = (from da in context.OrderItems
                            join du in context.UserorderRecound
                            on da.OrderId equals du.OrderId
                            join dd in context.SupplierBookInfo
                            on da.SupplierId equals dd.SupplierId
                            where da.SupplierId == SupplierId && du.PayStatues == "未支付"
                            select new { da.Isbn, da.SupplierId, da.BookName, da.BookPrice, da.Quantity, da.Statue, du.OrderId, du.BuyNum, du.ConsigName, du.ClinchTime, du.PayStatues }).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("data", list);
                dic.Add("count", list.Count);
                return JsonConvert.SerializeObject(dic);
            }
        }
        //待发货商品
        [HttpGet]
        public string GetGoodFH(string token = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;
                var list = (from da in context.OrderItems
                            join du in context.UserorderRecound
                            on da.OrderId equals du.OrderId
                            join dd in context.SupplierBookInfo
                            on da.SupplierId equals dd.SupplierId
                            where da.SupplierId == SupplierId && du.OrderStatue == "未发货"
                            select new { da.Isbn, da.SupplierId, da.BookName, da.BookPrice, da.Quantity, da.Statue, du.OrderId, du.BuyNum, du.ConsigName, du.ClinchTime, du.PayStatues, du.OrderStatue }).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("data", list);
                dic.Add("count", list.Count);
                return JsonConvert.SerializeObject(dic);
            }
        }
        //修改发货状态
        [HttpGet]
        public int UpdGoodFH(string token = "", string orderId = "")
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return 0;
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return 0;
                }
                var list = context.UserorderRecound.Where(n => n.OrderId.Equals(orderId)).FirstOrDefault();

                list.OrderStatue = "已发货";
                context.UserorderRecound.Update(list);
                return context.SaveChanges();
            }
        }

        //待确认商品
        [HttpGet]
        public string GetGoodSH(string token = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;
                var list = (from da in context.OrderItems
                            join du in context.UserorderRecound
                            on da.OrderId equals du.OrderId
                            join dd in context.SupplierBookInfo
                            on da.SupplierId equals dd.SupplierId
                            where da.SupplierId == SupplierId && du.DelivaeryStatue == "待确认收货"
                            select new { da.Isbn, da.SupplierId, da.BookName, da.BookPrice, da.Quantity, da.Statue, du.OrderId, du.BuyNum, du.ConsigName, du.ClinchTime, du.PayStatues, du.OrderStatue, du.DelivaeryStatue }).ToList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("data", list);
                dic.Add("count", list.Count);
                return JsonConvert.SerializeObject(dic);
            }
        }

        //public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        //{
        //    HashSet<TKey> seenKeys = new HashSet<TKey>();
        //    foreach (TSource element in source)
        //    {
        //        if (seenKeys.Add(keySelector(element)))
        //        {
        //            yield return element;
        //        }
        //    }
        //}
        //评价
        [HttpGet]
        public string GetGoodPJ(string token = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;

                var list = (from da in context.ReplyInfo
                            join dd in context.SupplierBookInfo
                            on da.SupplierId equals dd.SupplierId
                            where da.SupplierId == SupplierId
                            select new { da.Isbn, da.Replay }).ToList().GroupBy(a => a.Isbn).Select(a =>
                                new ReplyModel
                                {
                                    Isbn = a.Key,
                                    ReplyA = (from s in a select s.Replay).ToList()
                                }
                               );

                //var listResult = from s in list
                //                 select new ReplyModel
                //                 {
                //                     Isbn = s.Key,
                //                     ReplyA =  (from a in s select a.Replay).ToList()
                //                 };

                //   select new ReplyModel { ReplyA = new List<ReplyInfo> { new ReplyInfo { Replay = g.v } }, Isbn = g.Key.Isbn }).ToList();
                //var query = list.GroupBy(s => s.Isbn).Select(x => x.First()).ToList();
                //var linq=from s in context.ReplyInfo where 
                //string ss= JsonConvert.SerializeObject(list);
                //ReplyInfo reply = JsonConvert.DeserializeObject<ReplyInfo>(ss);
                //var query = reply.DistinctBy(p=>p.Isbn);

                //Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic.Add("data", list);
                //dic.Add("count", list.Count);
                return JsonConvert.SerializeObject(list);
            }
        }



        //商家信息
        [HttpGet]
        public string GetUserRoder(string token = "")
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                if (token == "undefined")
                {
                    return "";
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return "";
                }

                string SupplierId = ue.SuppLierId;
                var list = (from s in context.UserRoderInfo where s.SuppLierId == SupplierId select s).FirstOrDefault();
                return JsonConvert.SerializeObject(list);
            }
        }
        //修改商家信息
        [HttpPost]
        public int UpdUserRoder(Dictionary<string, object> dic)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                UserRoderInfo info = JsonConvert.DeserializeObject<UserRoderInfo>(dic["obj"].ToString());
                string token = dic["token"].ToString();
                if (token== "undefined")
                {
                    return 0;
                }
                JWTHelper jWT = new JWTHelper();
                UserRoderInfo user = JsonConvert.DeserializeObject<UserRoderInfo>(jWT.GetPayload(token));
                var ue = context.UserRoderInfo.Where(n => n.ContactName.Equals(user.ContactName) && n.SupplierPwd.Equals(user.SupplierPwd)).FirstOrDefault();
                if (ue == null)
                {
                    return 0;
                }

                string SupplierId = ue.SuppLierId;
                var list = (from s in context.UserRoderInfo where s.SuppLierId == SupplierId select s).FirstOrDefault();
                list.ShopName = info.ShopName;
                list.ShopAddress = info.ShopAddress;
                list.ContactName = info.ContactName;
                list.CortactPhone = info.CortactPhone;
                list.Mail = info.Mail;
                list.TrueName = info.TrueName;
                context.UserRoderInfo.Update(list);
                //context.Set<UserRoderInfo>().Where(n=>n.SuppLierId.Equals(SupplierId)).Update(info);
                //context.Entry<UserRoderInfo>(info).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}