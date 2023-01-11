using MediatR;

namespace StudentsManagement.Domain.Commands;

public record CreateCourseCommand(string CourseName, string? Description, IEnumerable<Guid> AssignedStudentsIds) : IRequest<int>;