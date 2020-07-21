using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class SupplierBookInfo
    {
        public string SupplierId { get; set; }
        public string Isbn { get; set; }
        public int? TotalQuantity { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public double? Score { get; set; }
        public string BookType { get; set; }
        public string Remark { get; set; }
        public int? SaledQuantity { get; set; }
        public string BookStues { get; set; }
        public int Gtid { get; set; }
    }
}
