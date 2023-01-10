using Microsoft.EntityFrameworkCore;
using StudentsManagement.Domain.Models;

namespace StudentsManager.Data.Data;

public sealed class StudentsManagementContext : DbContext
{
    public StudentsManagementContext(DbContextOptions<StudentsManagementContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Student> Students => Set<Student>();

    public DbSet<Course> Courses => Set<Course>();
}