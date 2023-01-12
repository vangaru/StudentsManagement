using System.ComponentModel.DataAnnotations;
using StudentsManagement.Domain.Models;

namespace StudentsManager.Mvc.Models;

public class UpdateCourseViewModel
{
    public Guid CourseId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(400)]
    public string? Description { get; set; }
    
    public SortedList<Guid, string> Students { get; } = new();

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<Guid> SelectedStudentsIds { get; set; } = new();

    public static UpdateCourseViewModel CreateInstance(Course course, IEnumerable<Student> allStudents)
    {
        var updateCourseViewModel = new UpdateCourseViewModel
        {
            CourseId = course.Id,
            Name = course.Name,
            Description = course.Description
        };

        foreach (Student student in allStudents)
        {
            updateCourseViewModel.Students.Add(student.Id, student.Name);
        }
        
        updateCourseViewModel.SelectedStudentsIds.AddRange(course.AssignedStudents.Select(c => c.Id));

        return updateCourseViewModel;
    }
}