using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class ReplyInfo
    {
        public string ReplyId { get; set; }
        public string SupplierId { get; set; }
        public string Isbn { get; set; }
        public string Replay { get; set; }
        public DateTime? ReplyTime { get; set; }
        public string AccountName { get; set; }
    }
}
