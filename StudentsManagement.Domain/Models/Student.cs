namespace StudentsManagement.Domain.Models;

public class Student
{
    private string? _name;
    
    public Guid Id { get; set; }

    public string Name
    {
        get => _name ?? throw new ApplicationException($"Field {_name} is required.");
        set => _name = value;
    }

    public List<Course> AssignedCourses { get; set; } = new();
}