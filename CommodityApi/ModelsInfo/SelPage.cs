using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommodityApi.ModelsInfo
{
    public class SelPage
    {

        public string Authorname { get; set; }
        public string Bookname { get; set; }
        public string Chu { get; set; }
        public string Typese { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 2;

        /// <summary>
        /// 价格升序
        /// </summary>
        public string AseInfo { get; set; }
        /// <summary>
        /// 销售降序
        /// </summary>
        public string DesOut { get; set; }




    }
}
