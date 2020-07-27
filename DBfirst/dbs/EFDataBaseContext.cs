using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBfirst.dbs
{
    public partial class EFDataBaseContext : DbContext
    {
        public EFDataBaseContext()
        {
        }

        public EFDataBaseContext(DbContextOptions<EFDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClassInfo> ClassInfo { get; set; }
        public virtual DbSet<StudentInfo> StudentInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;User Id=sa;Password=123456;Database=EFDataBase;Persist Security Info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassInfo>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PK__ClassInf__CB1927C077F38E05");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudentInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
