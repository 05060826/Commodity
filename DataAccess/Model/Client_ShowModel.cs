using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    /// <summary>
    /// 顾客页面显示
    /// </summary>
  public  class Client_ShowModel
    {
        public List<UserorderRecound> ShowList { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int ToTalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal { get; set; }
    }
}
