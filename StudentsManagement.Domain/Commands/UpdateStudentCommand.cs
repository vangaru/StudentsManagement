using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Commands;

public record UpdateStudentCommand(Student Student) : IRequest<int>;