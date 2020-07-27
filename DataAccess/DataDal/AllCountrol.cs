using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.DataModels;

namespace DataAccess.DataDal
{
  public  class AllCountrol
    {
        /// <summary>
        /// 顾客信息表
        /// </summary>
        EFHelper<Customer> cus = new EFHelper<Customer>();

        /// <summary>
        /// 显示顾客信息
        /// </summary>
        /// <returns></returns>
        public List<Customer> ShowCustor()
        {


            var listcu = cus.GetAll();

            return listcu;
        }



        /// <summary>
        /// 图书信息表
        /// </summary>
        EFHelper<BookInfo> Book = new EFHelper<BookInfo>();
        /// <summary>
        /// 显示所有图书信息
        /// </summary>
        /// <returns></returns>
        public List<BookInfo> GetAllInfo()
        {

            return Book.GetAll();
        
        
        }

        /// <summary>
        /// 添加新的图书信息
        /// </summary>
        /// <returns></returns>
        public int AddBookInfo(BookInfo mode)
        {
            return Book.Add(mode);
        
        }
        /// <summary>
        /// 显示详情或者反填图书信息
        /// </summary>
        /// <returns></returns>
        public BookInfo GetAllInfo(string isbn)
        {

            var list = Book.GetAll();

            BookInfo book = list.Where(m => m.Isbn.Contains(isbn)).FirstOrDefault();

            return book;


        }

        /// <summary>
        /// 修改图书信息
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int ModifyBookInfo(BookInfo mode)
        {


            return Book.Modify(mode);
        
        }

        /// <summary>
        /// 作者信息表
        /// </summary>
        EFHelper<AuthorInfo> auth = new EFHelper<AuthorInfo>();

        /// <summary>
        /// 显示作者信息
        /// </summary>
        /// <returns></returns>
        public  List<AuthorInfo> GetAllAuthorInfo()
        {
            var list = auth.GetAll();
          
            return list;

        }
        /// <summary>
        /// 显示作者信息详情
        /// </summary>
        /// <returns></returns>
        public AuthorInfo GetAllAuthor(string aname)
        {
            var list = auth.GetAll();
            AuthorInfo aut = list.Where(m => m.Aname.Contains(aname)).FirstOrDefault();
            return aut;
        
        }
        /// <summary>
        /// 作者信息添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddAuthorInfo(AuthorInfo mode)
        {

            return auth.Add(mode);
        
        
        }

        /// <summary>
        /// 作者信息修改
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int ModifyAuthorInfo(AuthorInfo mode)
        {

            return auth.Modify(mode);


        }
        /// <summary>
        /// 商家信息表
        /// </summary>
        EFHelper<UserRoderInfo> Roder = new EFHelper<UserRoderInfo>();


        /// <summary>
        /// 显示注册商家信息
        /// </summary>
        /// <returns></returns>
        public List<UserRoderInfo> AllRoderInfo()
        {

            return Roder.GetAll();
        }
        /// <summary>
        /// 注册商家信息
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddRoderInfo(UserRoderInfo mode)
        {

            return Roder.Add(mode);
        
        }
        /// <summary>
        /// 修改商家信息
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int ModifyRoderInfo(UserRoderInfo mode)
        {

            return Roder.Modify(mode);
        
        
        }


        /// <summary>
        /// 图书总分类表
        /// </summary>
        EFHelper<AllClassTypes> AllTypes = new EFHelper<AllClassTypes>();
        /// <summary>
        /// 显示图书总分类
        /// </summary>
        /// <returns></returns>
        public List<AllClassTypes> AllTypesInfo()
        {

            var listt= AllTypes.GetAll();

            return listt;
        
        
        }

        /// <summary>
        ///添加图书总分类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddAllTypesInfo(AllClassTypes mode)
        {

            return AllTypes.Add(mode);

        }

        /// <summary>
        /// 删除图书分类
        /// </summary>
        /// <returns></returns>
        public int DeleteAllInfo(AllClassTypes mode)
        {


            return AllTypes.Delete(mode);


        }

        /// <summary>
        /// 图书次分类信息表
        /// </summary>
        EFHelper<NextClassType> NextTypes = new EFHelper<NextClassType>();
        /// <summary>
        /// 显示图书分类
        /// </summary>
        /// <returns></returns>
        public List<NextClassType> AllNextClassInfo()
        {

            var listClass = NextTypes.GetAll();

            return listClass;


        }

        /// <summary>
        ///添加图书分类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddNextClassInfo(NextClassType mode)
        {

            return NextTypes.Add(mode);

        }

        /// <summary>
        ///修改图书分类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int ModifyNextClassInfo(NextClassType mode)
        {

            return NextTypes.Modify(mode);

        }


        /// <summary>
        /// 删除图书次级分类
        /// </summary>
        /// <returns></returns>
        public int DeleteInfo(NextClassType mode)
        {


            return NextTypes.Delete(mode);
        
        
        }

    }
}
