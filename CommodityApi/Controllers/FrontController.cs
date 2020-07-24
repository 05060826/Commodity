using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataDal;
using DataAccess.DataModels;
using DataAccess.Login;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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

                    jiami = jwtHeader.GetToken(keys, 3000000); 
                    ret.AccountName = login.AccountName;
                    ret.AccountPwd = login.AccountPwd;
                    ret.Quan = "尊敬的客户你好";
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
                if (supplier.Count == 0)
                {
                    Dictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ContactName", supplier[0].ContactName);
                    keys.Add("SupplierPwd", supplier[0].SupplierPwd);
                    keys.Add("SuppLierID", supplier[0].SuppLierId);

                    jiami = jwtHeader.GetToken(keys, 3000000);
                    ret.AccountName = login.AccountName;
                    ret.AccountPwd = login.AccountPwd;
                    ret.Quan = "尊敬的供应商你好";

                    ret.Encipherment = jiami;
                    ret.Name = supplier[0].TrueName;
                }
                return ret;
            }
            if (login.Table == "Administrator")
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
                if (supplier.Count == 0)
                {
                    Dictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("ManagerUserName", Administrator[0].ManagerUserName);
                    keys.Add("ManagerPwd", Administrator[0].ManagerPwd);
                    keys.Add("ManageId", Administrator[0].ManageId);

                    jiami = jwtHeader.GetToken(keys, 3000000);
                    ret.AccountName = login.AccountName;
                    ret.AccountPwd = login.AccountPwd;
                    ret.Quan = "尊敬的管理员你好";
                    ret.Encipherment = jiami;
                    ret.Name = Administrator[0].ManagerUserName;
                }
                return ret;
            }
            else
            {
                return ret;
            }

            //BankInfo bank = new BankInfo();
            //bank.BankCardMoney = 2;
            //bank.BankCardNum = "2";
            //bank.BankPwd = "2";
            //List<BankInfo> bb = context.BankInfo.ToList();

            //context.BankInfo.Add(bank);
            //context.SaveChanges();
        }

    }
}