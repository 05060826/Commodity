using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommodityApi.ModelsInfo
{
    public class BookPage
    {
        /// <summary>
        /// 图书信息
        /// </summary>
       public List<BooksInfoAll> BooksInfoAlls { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
    }
}
