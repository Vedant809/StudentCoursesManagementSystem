using Microsoft.EntityFrameworkCore;
using StudentCoursesSystem.Entities;

namespace StudentCoursesSystem.Repository
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Composite key
            modelBuilder.Entity<StudentCourses>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourses>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<StudentCourses>()
                .HasOne(x => x.Course)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
            
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeProjects)
                .HasForeignKey(x => x.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(x => x.Project)
                .WithMany(x => x.EmployeeProjects)
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
