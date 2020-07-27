using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataModels;
using System.Web;
using System.Data;

namespace DataAccess.DataDal
{
   public class EFHelper<T> where T:class,new()
    {
        CommercedataContext db = new CommercedataContext();

        //查询语句
        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }
        //添加数据
        public int Add(T t)
        {
            db.Set<T>().Add(t);
            return db.SaveChanges();

        }

        //删除数据
        public int Delete(T t)
        {
            db.Set<T>().Remove(t);
            return db.SaveChanges();

        }

        //修改
        public int Modify(T t)
        {
            db.Set<T>().Attach(t);//System.Data.Entity.EntityState.Modified;
                                  //   db.Entry(t).State = System.Data.
            db.Entry(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();

        }

    }
}
