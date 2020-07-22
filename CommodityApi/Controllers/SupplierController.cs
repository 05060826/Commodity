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
        [HttpGet]
        public List<NextClassType> GetTypeData()
        {
            using (CommercedataContext context = new CommercedataContext())
            {
                List<NextClassType> list = context.NextClassType.ToList();
                return list;
            }
        }
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
    }
}