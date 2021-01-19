using Hogwarts.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Data
{
    class HogwartsContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Course> Courses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Hogwarts;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(a => a.Student)
                .WithMany(m => m.Courses)
                .HasForeignKey(a => a.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(m => m.Course)
                .WithMany(a => a.Students)
                .HasForeignKey(m => m.CourseId);

        }
    }
}
