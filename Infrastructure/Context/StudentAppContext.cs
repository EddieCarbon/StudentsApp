using Application.Configuration.Services;
using Core.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class StudentAppContext : IdentityDbContext<ApplicationUser>
{
    private readonly UserResolverService _userService;
    public StudentAppContext(DbContextOptions<StudentAppContext> options, UserResolverService userService) : base(options)
    {
        _userService = userService;
    }
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((AuditableEntity)entityEntry.Entity).lastModified = DateTime.UtcNow;
            ((AuditableEntity)entityEntry.Entity).LastModifiedBy = _userService.GetUser();

            if (entityEntry.State == EntityState.Added)
            {
                ((AuditableEntity)entityEntry.Entity).Created = DateTime.UtcNow;
                ((AuditableEntity)entityEntry.Entity).CreatedBy = _userService.GetUser();
            }
        }

        return await base.SaveChangesAsync();
    }

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

