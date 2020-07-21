using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class BankInfo
    {
        public string BankCardNum { get; set; }
        public string BankPwd { get; set; }
        public double? BankCardMoney { get; set; }
    }
}
