using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Util
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(){}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options){}

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Erollment> Erollment { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20); 

                entity.OwnsOne(
                    e => e.Credit,
                    p => p.Property(kv => kv.Id).HasColumnName("Credit"));
            });

            modelBuilder.Entity<Erollment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

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
                entity.OwnsOne(
                    e => e.TestProperty,
                    p => p.Property(kv => kv.Id).HasColumnName("TestPropertyId"));
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
