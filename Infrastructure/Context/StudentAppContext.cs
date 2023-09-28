using Core.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class StudentAppContext : IdentityDbContext<ApplicationUser>
{
    public StudentAppContext(DbContextOptions<StudentAppContext> options) : base(options)
    {
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Product
        // The entity type 'IdentityUserLogin<string>' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'. For more information on keyless entity types, see https://go.microsoft.com/fwlink/?linkid=2141943.
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Department>().ToTable("Department");
        modelBuilder.Entity<Student>().HasKey(s => s.Id);
        modelBuilder.Entity<Student>()
            .Property(p => p.Name)
            .HasMaxLength(20)
            .IsRequired();
        modelBuilder.Entity<Student>()
            .Property(p => p.Surname)
            .HasMaxLength(40)
            .IsRequired();
        modelBuilder.Entity<Student>()
            .Property(p => p.Email)
            .HasMaxLength(80)
            .IsRequired();
        modelBuilder.Entity<Student>()
            .Property(p => p.DateOfBirth)
            .IsRequired();
        modelBuilder.Entity<Student>()
            .Property(p => p.StartingStudyYear)
            .IsRequired();
        modelBuilder.Entity<Student>()
            .HasOne<StudentAddress>(s => s.Address)
            .WithOne(ad => ad.Student)
            .HasForeignKey<StudentAddress>(ad => ad.StudentId);
        modelBuilder.Entity<Student>()
           .HasOne<Department>(s => s.Department)
           .WithMany(g => g.Students)
           .HasForeignKey(s => s.CurrentDepartmentId);

        /* modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
         modelBuilder.Entity<StudentCourse>()
             .HasOne<Student>(sc => sc.Student)
             .WithMany(s => s.StudentCourses)
             .HasForeignKey(sc => sc.StudentId);
         modelBuilder.Entity<StudentCourse>()
             .HasOne<Course>(sc => sc.Course)
             .WithMany(s => s.StudentCourses)
             .HasForeignKey(sc => sc.CourseId);*/
        #endregion
    }
}
