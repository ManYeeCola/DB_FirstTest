using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects.Aspect
{
    public partial class University_TestContext : DbContext
    {
        public University_TestContext()
        {
        }

        public University_TestContext(DbContextOptions<University_TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Erollment> Erollment { get; set; }
        public virtual DbSet<JctStudentResearchArea> JctStudentResearchArea { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<TblStudent> TblStudent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.18.51\\FT,1435;;Database=University_Test;uid=syspro;pwd=syspro");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Erollment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");
            });

            modelBuilder.Entity<JctStudentResearchArea>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("jctStudentResearchArea");

                entity.HasIndex(e => new { e.StudentId, e.ResearchAreaId })
                    .HasName("IX_jctStudentResearchAre")
                    .IsUnique();

                entity.Property(e => e.ResearchAreaId).HasColumnName("ResearchAreaID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.Name })
                    .HasName("IX_Name");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblStudent>(entity =>
            {
                entity.ToTable("tblStudent");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_tblStudent");

                entity.HasIndex(e => new { e.PhoneNum, e.PhoneNumViceOne, e.PhoneNumViceTwo })
                    .HasName("IX_tblStudent_UN_PhoneNum_NOTNULL")
                    .IsUnique()
                    .HasFilter("([PhoneNum_ViceOne] IS NOT NULL AND [PhoneNum_ViceTwo] IS NOT NULL)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.HospitalName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumViceOne).HasColumnName("PhoneNum_ViceOne");

                entity.Property(e => e.PhoneNumViceTwo).HasColumnName("PhoneNum_ViceTwo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
