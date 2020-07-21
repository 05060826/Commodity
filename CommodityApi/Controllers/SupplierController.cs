using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommodityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        public List<BookInfo> GetBookInfo()
        {
            using (CommercedataContext context=new CommercedataContext())
            {
                List<BookInfo>  list=context.BookInfo.ToList();
                return list;
            }
        }
    }
}