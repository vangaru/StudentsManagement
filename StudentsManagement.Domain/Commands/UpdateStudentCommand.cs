using MediatR;

namespace StudentsManagement.Domain.Commands;

public record UpdateStudentCommand(Guid StudentId, string StudentName, IEnumerable<Guid> AssignedCoursesIds) : IRequest<int>;