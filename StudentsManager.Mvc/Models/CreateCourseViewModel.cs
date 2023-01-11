using System.ComponentModel.DataAnnotations;
using StudentsManagement.Domain.Models;

namespace StudentsManager.Mvc.Models;

public class CreateCourseViewModel
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(400)]
    public string? Description { get; set; }
    
    public SortedList<Guid, string> Students { get; } = new();

    public List<Guid> SelectedStudentsIds { get; set; } = new();

    public static CreateCourseViewModel CreateInstance(IEnumerable<Student> students)
    {
        var viewModel = new CreateCourseViewModel();
        foreach (Student student in students)
        {
            viewModel.Students.Add(student.Id, student.Name);
        }

        return viewModel;
    }
}