using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommodityApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class FrontController : ControllerBase
    {

        [Route("insert")]
        [HttpGet]
        public void Insert()
        {
            CommercedataContext context = new CommercedataContext();
            BankInfo bank = new BankInfo();
            bank.BankCardMoney = 2;
            bank.BankCardNum = "2";
            bank.BankPwd = "2";
            List<BankInfo> bb = context.BankInfo.ToList();

            context.BankInfo.Add(bank);
            context.SaveChanges();
        }



    }
}