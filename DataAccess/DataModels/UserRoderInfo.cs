using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class UserRoderInfo
    {
        public string SuppLierId { get; set; }
        public string SupplierPwd { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string ContactName { get; set; }
        public string CortactPhone { get; set; }
        public DateTime? RegDate { get; set; }
        public string Mail { get; set; }
        public string TrueName { get; set; }
        public double? AllSaledAccount { get; set; }
        public int? QuanTity { get; set; }
        public string SuoolierType { get; set; }
        public string Remark { get; set; }
    }
}
