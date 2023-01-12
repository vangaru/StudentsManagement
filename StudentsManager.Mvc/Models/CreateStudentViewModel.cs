using System.ComponentModel.DataAnnotations;
using StudentsManagement.Domain.Models;

namespace StudentsManager.Mvc.Models;

public class CreateStudentViewModel
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    
    public SortedList<Guid, string> Courses { get; } = new();

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Guid> SelectedCoursesIds { get; set; } = new();

    public static CreateStudentViewModel CreateInstance(IEnumerable<Course> courses)
    {
        var viewModel = new CreateStudentViewModel();
        foreach (Course course in courses)
        {
            viewModel.Courses.Add(course.Id, course.Name);
        }

        return viewModel;
    }
}