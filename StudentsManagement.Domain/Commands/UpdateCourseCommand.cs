using MediatR;

namespace StudentsManagement.Domain.Commands;

public record UpdateCourseCommand(Guid CourseId, string CourseName, string? CourseDescription, IEnumerable<Guid> AssignedStudentsIds) 
    : IRequest<int>;