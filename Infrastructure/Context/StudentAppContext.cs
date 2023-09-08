using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class StudentAppContext : DbContext
{
    public StudentAppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Product

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
