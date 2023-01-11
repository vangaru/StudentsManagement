using MediatR;

namespace StudentsManagement.Domain.Commands;

public record DeleteStudentCommand(Guid StudentId) : IRequest<int>;