using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.DataDal;

namespace CommodityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        AllCountrol dal = new AllCountrol();
      
            ////查询语句
            //public List<Customer> GetAll()
            //{

            //var list = dal.GetAll();
            //return list;

            //}
       
    }
}