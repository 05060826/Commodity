using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.DataModels
{
    public partial class CommercedataContext : DbContext
    {
        public CommercedataContext()
        {
        }

        public CommercedataContext(DbContextOptions<CommercedataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllClassTypes> AllClassTypes { get; set; }
        public virtual DbSet<AuthorInfo> AuthorInfo { get; set; }
        public virtual DbSet<BankInfo> BankInfo { get; set; }
        public virtual DbSet<Bankfround> Bankfround { get; set; }
        public virtual DbSet<BookInfo> BookInfo { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<ManageInfo> ManageInfo { get; set; }
        public virtual DbSet<NextClassType> NextClassType { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<ReplyInfo> ReplyInfo { get; set; }
        public virtual DbSet<SupplierBookInfo> SupplierBookInfo { get; set; }
        public virtual DbSet<UserRoderInfo> UserRoderInfo { get; set; }
        public virtual DbSet<UserorderRecound> UserorderRecound { get; set; }
        public virtual DbSet<UserorderReound> UserorderReound { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.1.100;Database=Commercedata;uid=sa;pwd=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllClassTypes>(entity =>
            {
                entity.HasKey(e => e.AllClassId)
                    .HasName("PK_ALLCLASSTYPES");

                entity.Property(e => e.AllClassId)
                    .HasColumnName("AllClass_Id")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AllClassName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuthorInfo>(entity =>
            {
                entity.HasKey(e => e.AuthorId)
                    .HasName("PK_AUTHORINFO");

                entity.Property(e => e.AuthorId)
                    .HasColumnName("Author_Id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Aname)
                    .IsRequired()
                    .HasColumnName("AName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Introduction)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Nation)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BankInfo>(entity =>
            {
                entity.HasKey(e => e.BankCardNum)
                    .HasName("PK_BANKINFO");

                entity.Property(e => e.BankCardNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BankPwd)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bankfround>(entity =>
            {
                entity.HasKey(e => e.AccTitvityId)
                    .HasName("PK_BANKFROUND");

                entity.Property(e => e.AccTitvityId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ActityContent)
                    .HasColumnName("Actity_Content")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ActivityTitle)
                    .HasColumnName("activityTitle")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StarTime).HasColumnType("datetime");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookInfo>(entity =>
            {
                entity.HasKey(e => e.Isbn)
                    .HasName("PK_BOOKINFO");

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorId)
                    .HasColumnName("Author_Id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Introduction)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NclassId)
                    .HasColumnName("NClassId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Publish)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PublishTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK_CUSTOMER");

                entity.Property(e => e.AccountId).ValueGeneratedNever();

                entity.Property(e => e.AccountEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountPwd)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UseTel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserAddres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserReddate).HasColumnType("datetime");

                entity.Property(e => e.UserSex)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ManageInfo>(entity =>
            {
                entity.HasKey(e => e.ManageId)
                    .HasName("PK_MANAGEINFO");

                entity.Property(e => e.ManageId).ValueGeneratedNever();

                entity.Property(e => e.ManagerPwd)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerUserName)
                    .HasColumnName("Manager_UserName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NextClassType>(entity =>
            {
                entity.HasKey(e => e.NclassId)
                    .HasName("PK_NEXTCLASS_TYPE");

                entity.ToTable("NextClass_Type");

                entity.Property(e => e.NclassId)
                    .HasColumnName("NClassId")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AllId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.NclassName)
                    .HasColumnName("NClassName")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => e.OrderitemId)
                    .HasName("PK_ORDERITEMS");

                entity.Property(e => e.OrderitemId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.BookName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookPrice).HasColumnName("Book_Price");

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_Id")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Statue)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReplyInfo>(entity =>
            {
                entity.HasKey(e => e.ReplyId)
                    .HasName("PK_REPLYINFO");

                entity.Property(e => e.ReplyId)
                    .HasColumnName("Reply_Id")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Replay)
                    .HasColumnName("replay")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReplyTime).HasColumnType("datetime");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("Supplier_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierBookInfo>(entity =>
            {
                entity.HasKey(e => e.Gtid)
                    .HasName("PK_SUPPLIERBOOKINFO");

                entity.Property(e => e.Gtid)
                    .HasColumnName("GTId")
                    .ValueGeneratedNever();

                entity.Property(e => e.BookStues)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isbn)
                    .HasColumnName("ISBN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SaledQuantity).HasColumnName("Saled_Quantity");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("Supplier_ID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalQuantity).HasColumnName("Total_quantity");
            });

            modelBuilder.Entity<UserRoderInfo>(entity =>
            {
                entity.HasKey(e => e.SuppLierId)
                    .HasName("PK_USERRODERINFO");

                entity.Property(e => e.SuppLierId)
                    .HasColumnName("SuppLierID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AllSaledAccount).HasColumnName("All_Saled_account");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CortactPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate)
                    .HasColumnName("Reg_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShopAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShopName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuoolierType)
                    .HasColumnName("Suoolier_Type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierPwd)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrueName)
                    .HasColumnName("True_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserorderRecound>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_USERORDERRECOUND");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerFreebAck)
                    .HasColumnName("BuyerFreebACK")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ClinchTime).HasColumnType("datetime");

                entity.Property(e => e.ConfirmTime).HasColumnType("datetime");

                entity.Property(e => e.ConsigAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ConsigName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ConsigTel)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.DelivaeryStatue)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryTime).HasColumnType("datetime");

                entity.Property(e => e.OrderStatue)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PayStatues)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PayTime).HasColumnType("datetime");

                entity.Property(e => e.ReplayStatue)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserorderReound>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_ID")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnAcooungd).HasColumnName("Return_Acooungd");

                entity.Property(e => e.ReturnId)
                    .HasColumnName("Return_Id")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnPerson)
                    .HasColumnName("Return_Person")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
