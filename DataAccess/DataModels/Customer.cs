using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class Customer
    {
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public double? BuyAmunt { get; set; }
        public string AccountPwd { get; set; }
        public string UserName { get; set; }
        public string UserSex { get; set; }
        public int? UseAge { get; set; }
        public string UseTel { get; set; }
        public string UserAddres { get; set; }
        public string AccountEmail { get; set; }
        public DateTime? UserReddate { get; set; }
        public string Remark { get; set; }
        public int AccountId { get; set; }
    }
}
