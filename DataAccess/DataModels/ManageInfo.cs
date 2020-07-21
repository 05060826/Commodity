using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class ManageInfo
    {
        public string ManagerUserName { get; set; }
        public string ManagerPwd { get; set; }
        public string Remark { get; set; }
        public int ManageId { get; set; }
    }
}
