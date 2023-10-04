using Core.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Context;

public class StudentAppContext : IdentityDbContext<ApplicationUser>
{
    public StudentAppContext(DbContextOptions<StudentAppContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Department>().ToTable("Departments");

        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id);

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
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.CurrentDepartmentId);

        base.OnModelCreating(modelBuilder);
    }
}

