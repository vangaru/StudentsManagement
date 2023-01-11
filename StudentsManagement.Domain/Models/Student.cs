namespace StudentsManagement.Domain.Models;

public class Student
{
    private string? _name;
    
    public Guid Id { get; set; }

    public string Name
    {
        get => _name ?? throw new ApplicationException($"Field Name is required in {typeof(Student)}.");
        set => _name = value;
    }

    public List<Course> AssignedCourses { get; set; } = new();
}