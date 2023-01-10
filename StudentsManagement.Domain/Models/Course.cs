namespace StudentsManagement.Domain.Models;

public class Course
{
    private string? _name;

    public Guid Id { get; set; }
    
    public string? Description { get; set; }

    public string Name
    {
        get => _name ?? throw new ApplicationException($"Field {_name} is required.");
        set => _name = value;
    }

    public List<Student> AssignedStudents { get; set; } = new();
}