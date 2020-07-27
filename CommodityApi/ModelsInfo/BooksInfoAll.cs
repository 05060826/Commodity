using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommodityApi.ModelsInfo
{
    public class BooksInfoAll
    {

        public string           Isbn                { get; set; }
        public string           AuthorId          { get; set; }
        public string           Aname                 { get; set; }
        public string           NclassId              { get; set; }
        public string           NclassName            { get; set; }
        public string           Title                 { get; set; }
        public string           Publish               { get; set; }
        public DateTime?        PublishTime            { get; set; }

        public double?          Price                    { get; set; }

        public int?             SaledNum                    { get; set; }





    }
}
