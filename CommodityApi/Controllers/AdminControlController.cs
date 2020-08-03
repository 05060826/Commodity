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
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace CommodityApi.Controllers
{
    [Route("api/[controller]/[action]")]

    [ApiController]
    public class AdminControlController : ControllerBase
    {

       AllCountrol dal = new AllCountrol();

        private IWebHostEnvironment _hostEnvironment;
        public AdminControlController(IWebHostEnvironment hostEnvironment)
        {
            
            _hostEnvironment = hostEnvironment;
        }

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
                               Aname = a.Aname,
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
        public int AddBookInfo(string obj)
        {
            BookInfo b = JsonConvert.DeserializeObject<BookInfo>(obj);
            var s = b.Image;
            if (Request.Form.Files.Count > 0)
            {
                //获取物理路径 webtootpath
                string path = _hostEnvironment.ContentRootPath + "\\wwwroot\\Image";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var file = Request.Form.Files[0];
                string fileExt = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string filename = Guid.NewGuid().ToString() + "." + fileExt;
                string fileFullName = path + "\\" + filename;
                using (FileStream fs = System.IO.File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                b.Image = "/img/" + filename;
            }

            return dal.AddBookInfo(b);

        }


        [HttpPost]
        /// <summary>
        /// 删除图书信息
        /// </summary>
        /// <returns></returns>

        public int DeleteBook(string isbn)
        {

            var listbook =dal.GetAllInfo();


            BookInfo mode = listbook.Where(m => m.Isbn.Contains(isbn)).FirstOrDefault();

            return dal.DeleteBookInfo(mode);






        }




        //-------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        /// <summary>
        /// 显示作者信息详情
        /// </summary>
        /// <returns></returns>
        public AuthorInfo GetAllAuthor(string aname)
        {
            var list = dal.GetAllAuthor(aname);

            return list;
        }
        [HttpPost]
        /// <summary>
        /// 作者信息添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddAuthorInfo(string obj)
        {
            AuthorInfo mode = JsonConvert.DeserializeObject<AuthorInfo>(obj);


            return dal.AddAuthorInfo(mode);


        }
       

        [HttpGet]
        /// <summary>
        /// 显示作者信息列表
        /// </summary>
        /// <returns></returns>
        
        
        public List<AuthorBook> GetAuthorInfo()
        {
            var booklist = dal.GetAllInfo().ToList();
            var authorlist = dal.GetAllAuthorInfo().ToList();

            var list = (from s in booklist
                        join a in authorlist on s.AuthorId equals a.AuthorId
                        select new AuthorBook
                        {
                            AuthorId=a.AuthorId,
                            Aname=a.Aname,
                            Age=a.Age,
                            Sex=a.Sex,
                            Nation=a.Nation,
                            Introduction=a.Introduction,
                            AllSaledNum=a.AllSaledNum,
                            Isbn=s.Isbn,
                            Title=s.Title,

                        }).ToList();
            return list;
        
        
        
        }


        /// <summary>
        ///图书类别 ---------------------------------------------------------------------------------------------------------------
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


        [HttpPost]
        /// <summary>
        /// 删除图书分类
        /// </summary>
        /// <returns></returns>
        public int DeleteAllInfo(string tid)
        {



            return 1;

        }



        //---------------------------------------------------------------------------------------------------------



        [HttpGet]
        /// <summary>
        /// 显示注册商家信息
        /// </summary>
        /// <returns></returns>
        public List<UserRoderInfo> AllRoderInfo()
        {

            var list= dal.AllRoderInfo();

            return list;
                
        }
        [HttpGet]
        /// <summary>
        /// 反填商家信息
        /// </summary>
        /// <returns></returns>
        public UserRoderInfo ModifySupplier(string suppliserid)
        {

            var listsupplier = dal.AllRoderInfo().ToList();
            UserRoderInfo mode = listsupplier.Where(m => m.SuppLierId.Contains(suppliserid)).FirstOrDefault();

            //if (mode.AllSaledAccount >= 1000 && mode.AllSaledAccount <= 2000)
            //{
            //    mode.QuanTity = 40;
            //    mode.SuoolierType = "会员商家";
            //}
            //else if (mode.AllSaledAccount < 1000)
            //{
            //    mode.QuanTity = 20;
            //    mode.SuoolierType = "普通商家";
            //}
            //else
            //{
            //    mode.QuanTity = 60;
            //    mode.SuoolierType = "超级商家";


            //}


            return mode;
        
        
        }
        [HttpPost]
        public int ModifyInfo(string  mode)
        {
            UserRoderInfo mods = JsonConvert.DeserializeObject<UserRoderInfo>(mode);



            return dal.ModifyRoderInfo(mods);




        }



       

        [HttpGet]

        /// <summary>
        /// 显示顾客信息
        /// </summary>
        /// <returns></returns>
        public List<Customer> ShowCustor()
        {


            


            var listcu = dal.ShowCustor();

            return listcu;
        }

    }
}
