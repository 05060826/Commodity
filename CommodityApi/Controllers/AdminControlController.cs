using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.DataDal;
using DataAccess.DataModels;
using CommodityApi.ModelsInfo;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace CommodityApi.Controllers
{
    [Route("api/[controller]/[action]")]

    [ApiController]
    public class AdminControlController : ControllerBase
    {

        AllCountrol dal = new AllCountrol();

        [HttpGet]
        /// <summary>
        /// 显示所有图书信息
        /// </summary>
        /// <returns></returns>
        public List<BooksInfoAll> GetAllInfo()
        {

            //图书信息
            var booklist = dal.GetAllInfo();
            //图书类别
            var typeslist = dal.AllNextClassInfo();
            //作者信息
            var authoList = dal.GetAllAuthorInfo();

            BooksInfoAll booksInfoAll = new BooksInfoAll();

            var listall = (from b in booklist
                           join t in typeslist on b.NclassId equals t.NclassId
                           join a in authoList on b.AuthorId equals a.AuthorId
                           select new BooksInfoAll
                           {
                               Isbn=b.Isbn,
                            
                               Aname=a.Aname,
                               NclassId=t.NclassId,
                               NclassName =t.NclassName,
                               Title=b.Title,
                               Publish=b.Publish,
                               PublishTime=b.PublishTime,
                               Price=b.Price,

                               SaledNum=b.SaledNum,



                           }).ToList();
            return listall;


        }

        [HttpGet]

        //图书分页
        public BookPage ShowPage(string  modes)
        {
            SelPage mode = JsonConvert.DeserializeObject<SelPage>(modes);

            var list = GetAllInfo().ToList(); ;


            //mode.PageIndex = 1;
            //mode.PageSize = 3;
            //作者查询
            if (mode.Authorname != null)
            {
                list = list.Where(m => m.Aname.Contains(mode.Authorname)).ToList();
            }
            else
            {

                list = GetAllInfo().ToList();
            
            }

            //书名查询
            if (mode.Bookname!=null)
            {
                list = list.Where(m => m.Title.Contains(mode.Bookname)).ToList();
            }
            else
            {

                list = GetAllInfo().ToList();

            }
            //出版社查询
            if (mode.Chu != null)
            {
                list = list.Where(m => m.Publish.Contains(mode.Chu)).ToList();
            }
            else
            {

                list = GetAllInfo().ToList();

            }

            //分类查询
            if (mode.Typese != null)
            {
                list = list.Where(m => m.NclassId.Contains(mode.Typese)).ToList();
            }
            else
            {

                list = GetAllInfo().ToList();

            }


            if (mode.AseInfo=="ase")
            {
                list = list.OrderBy(m => m.Price).ToList();
            }
            if (mode.DesOut=="des")
            {
                list = list.OrderByDescending(m => m.SaledNum).ToList();
            }



            //总条数
            var totalcount = list.Count();
            //总页数
            var allPage = totalcount / mode.PageSize + (totalcount % mode.PageSize > 0 ? 1 : 0);
            list = list.Skip(mode.PageSize * (mode.PageIndex - 1)).Take(mode.PageSize).ToList();



            BookPage bok = new BookPage();
            bok.BooksInfoAlls = list;
            bok.TotalCount = totalcount;
            bok.TotalPage = allPage;

            return bok;




        }


       
       

        [HttpPost]

        /// <summary>
        /// 添加新的图书信息
        /// </summary>
        /// <returns></returns>
        public int AddBookInfo(BookInfo mode)
        {


            return dal.AddBookInfo(mode);

        }



        //-------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 显示作者信息详情
        /// </summary>
        /// <returns></returns>
        public AuthorInfo GetAllAuthor(string aname)
        {
            var list = dal.GetAllAuthor(aname);

            return list;
        }

        /// <summary>
        /// 作者信息添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddAuthorInfo(AuthorInfo mode)
        {

            return dal.AddAuthorInfo(mode);


        }




        /// <summary>
        /// ---------------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        /// <summary>
        /// 显示图书分类
        /// </summary>
        /// <returns></returns>
        public List<AllTypeInfoClass> AllNextClassInfo()
        {
            var alllist = dal.AllTypesInfo().ToList();

            var nextlist = dal.AllNextClassInfo().ToList();

            List<AllTypeInfoClass> listallType = (from a in alllist join
                                                  n in nextlist
                                                  on a.AllClassId equals n.AllId
                                                  select new AllTypeInfoClass
                                                  {
                                                   NclassId=n.NclassId,
                                                   AllId=a.AllClassId,
                                                   NclassName=n.NclassName,


                                                   AllClassName=a.AllClassName


                                                  }).ToList();

            return listallType;


        }

        [HttpPost]

        /// <summary>
        ///添加图书总分类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddAllTypesInfo(AllClassTypes mode)
        {

            return dal.AddAllTypesInfo(mode);

        }
        [HttpGet]
        /// <summary>
        /// 显示图书总分类
        /// </summary>
        /// <returns></returns>
        public List<AllClassTypes> AllTypesInfo()
        {

            var listt = dal.AllTypesInfo().ToList();

            return listt;


        }
        [HttpPost]
        /// <summary>
        ///添加图书次分类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddNextClassInfo(NextClassType mode)
        {

            return dal.AddNextClassInfo(mode);

        }



        /// <summary>
        /// 删除图书分类
        /// </summary>
        /// <returns></returns>
        public int DeleteAllInfo(string tid)
        {



            return 1;

        }




    }
}
