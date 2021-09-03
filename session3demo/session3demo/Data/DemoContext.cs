using Microsoft.EntityFrameworkCore;
using session3demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace session3demo.Data
{
    public partial class DemoContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses{ get; set; }
        public DemoContext()
        {
        }

        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(e => e.StudentCourses)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.courseName);

            });
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(e => e.Courses)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.studentName);

            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentsCourses");
                entity.HasKey(e => new { e.studentName , e.courseName });
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
