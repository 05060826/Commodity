using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class Bankfround
    {
        public string AccTitvityId { get; set; }
        public string SupplierId { get; set; }
        public string ActivityTitle { get; set; }
        public string ActityContent { get; set; }
        public DateTime? StarTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? Discount { get; set; }
    }
}
