using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class UserorderRecound
    {
        public string OrderId { get; set; }
        public int? AccountId { get; set; }
        public string ConsigName { get; set; }
        public string ConsigAddress { get; set; }
        public string ConsigTel { get; set; }
        public double? SaledMoney { get; set; }
        public int? BuyNum { get; set; }
        public DateTime? ClinchTime { get; set; }
        public DateTime? PayTime { get; set; }
        public DateTime? ConfirmTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string BuyerFreebAck { get; set; }
        public string OrderStatue { get; set; }
        public string PayStatues { get; set; }
        public string DelivaeryStatue { get; set; }
        public string ReplayStatue { get; set; }
        public string SupplierId { get; set; }
    }
}
