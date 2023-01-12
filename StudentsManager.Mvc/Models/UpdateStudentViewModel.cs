using System.ComponentModel.DataAnnotations;
using StudentsManagement.Domain.Models;

namespace StudentsManager.Mvc.Models;

public class UpdateStudentViewModel
{
    public Guid StudentId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    
    public SortedList<Guid, string> Courses { get; } = new();

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Guid> SelectedCoursesIds { get; set; } = new();

    public static UpdateStudentViewModel CreateInstance(Student student, IEnumerable<Course> allCourses)
    {
        var updateStudentViewModel = new UpdateStudentViewModel
        {
            StudentId = student.Id,
            Name = student.Name
        };

        foreach (Course course in allCourses)
        {
            updateStudentViewModel.Courses.Add(course.Id, course.Name);
        }
        
        updateStudentViewModel.SelectedCoursesIds.AddRange(student.AssignedCourses.Select(s => s.Id));

        return updateStudentViewModel;
    }
}