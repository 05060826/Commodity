using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
  public  class UserShowModel
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
