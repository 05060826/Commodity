using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CommodityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        //可售图书
        [HttpGet]
        public string GetBookInfo(string typeId,string authorName="" ,string title ="" ,string pubish = "",int pageName =1 ,int limitName =10 )
        {
            using (CommercedataContext context=new CommercedataContext())
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
                    list= list.Where(n => n.Aname.Contains(authorName)).ToList();
                }
                if (!string.IsNullOrEmpty(title))
                {
                    list=list.Where(n => n.Title.Contains(title)).ToList();
                }
                if (!string.IsNullOrEmpty(pubish))
                {
                    list= list.Where(n => n.Publish.Contains(pubish)).ToList();
                }
                var slist= list.Skip((pageName - 1) * limitName).Take(limitName).ToList();
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
                            where da.Isbn==Isbn
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, da.Price, da.Revision, da.Page,du.AuthorId, du.Aname, dd.NclassId, dd.NclassName }).FirstOrDefault();
                return JsonConvert.SerializeObject(list);
            }
        }
        //添加到仓库
        [HttpPost]
        public int InsertBook(SupplierBookInfo info)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from s in context.SupplierBookInfo where s.Isbn == info.Isbn select s).FirstOrDefault();
                if (list==null)
                {
                    info.Score = 0;
                    info.SaledQuantity = 0;
                    info.BookStues = "未出售";
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
        public string GetCkBookInfo(string authorName = "", string title = "", string pubish = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from da in context.BookInfo
                            join du in context.AuthorInfo
                            on da.AuthorId equals du.AuthorId
                            join dd in context.SupplierBookInfo
                            on da.Isbn equals dd.Isbn
                            where dd.BookStues=="未出售"
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, dd.Price,dd.TotalQuantity,dd.Discount,dd.BookType,dd.SaledQuantity,dd.BookStues, du.AuthorId, du.Aname }).ToList();
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
                            select new { da.Isbn, da.Title, da.Publish, da.PublishTime, dd.Price, dd.TotalQuantity, dd.Discount, dd.BookType, dd.SaledQuantity, dd.BookStues, du.AuthorId, du.Aname,dc.NclassName }).FirstOrDefault();
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
        public int UpdBook(SupplierBookInfo info)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
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
        public string GetCkZBookInfo(string authorName = "", string title = "", string pubish = "", int pageName = 1, int limitName = 10)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from da in context.BookInfo
                            join du in context.AuthorInfo
                            on da.AuthorId equals du.AuthorId
                            join dd in context.SupplierBookInfo
                            on da.Isbn equals dd.Isbn
                            where dd.BookStues == "出售中"
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
        public int UpdxBookSate(string Isbn)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from s in context.SupplierBookInfo where s.Isbn == Isbn select s).FirstOrDefault();
                list.BookStues = "未出售";
                context.SupplierBookInfo.Update(list);
                return context.SaveChanges();
            }
        }
        //商家信息
        [HttpGet]
        public string GetUserRoder(string SupperId)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from s in context.UserRoderInfo where s.SuppLierId == SupperId select s).FirstOrDefault();
                return JsonConvert.SerializeObject(list);
            }
        }
        //修改商家信息
        [HttpPost]
        public int UpdUserRoder(UserRoderInfo info)
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                var list = (from s in context.UserRoderInfo where s.SuppLierId == info.SuppLierId select s).FirstOrDefault();
                list.ShopName = info.ShopName;
                list.ShopAddress = info.ShopAddress;
                list.ContactName = info.ContactName;
                list.CortactPhone = info.CortactPhone;
                list.Mail = info.Mail;
                list.TrueName = info.TrueName;
                context.UserRoderInfo.Update(list);
                return context.SaveChanges();
            }
        }
    }
}