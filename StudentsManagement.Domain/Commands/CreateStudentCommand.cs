using MediatR;

namespace StudentsManagement.Domain.Commands;

public record CreateStudentCommand(string StudentName, IEnumerable<Guid> AssignedCoursesIds) : IRequest<int>;