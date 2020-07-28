using System;
using System.Collections.Generic;

namespace DataAccess.DataModels
{
    public partial class BookInfo
    {
        public string Isbn { get; set; }
        public string AuthorId { get; set; }

        
        public string NclassId { get; set; }
         
        public string Title { get; set; }
        public string Publish { get; set; }
        public DateTime? PublishTime { get; set; }
        public int? Revision { get; set; }
        public int? Page { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
        public string Introduction { get; set; }
        public string Country { get; set; }
        public int? SaledNum { get; set; }
    }
}
