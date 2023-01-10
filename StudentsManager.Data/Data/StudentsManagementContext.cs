using Microsoft.EntityFrameworkCore;
using StudentsManagement.Domain.Models;

namespace StudentsManager.Data.Data;

public class StudentsManagementContext : DbContext
{
    public StudentsManagementContext(DbContextOptions<StudentsManagementContext> options) : base(options)
    {
    }
    
    public DbSet<Student> Students => Set<Student>();

    public DbSet<Course> Courses => Set<Course>();
}