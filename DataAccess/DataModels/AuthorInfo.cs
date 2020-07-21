using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class AuthorInfo
    {
        public string AuthorId { get; set; }
        public string Aname { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
        public string Nation { get; set; }
        public string Introduction { get; set; }
        public int? AllSaledNum { get; set; }
    }
}
