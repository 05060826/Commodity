using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class OrderItems
    {
        public string OrderitemId { get; set; }
        public string OrderId { get; set; }
        public string SupplierId { get; set; }
        public string Isbn { get; set; }
        public string BookName { get; set; }
        public double? BookPrice { get; set; }
        public int? Quantity { get; set; }
        public string Statue { get; set; }
    }
}
