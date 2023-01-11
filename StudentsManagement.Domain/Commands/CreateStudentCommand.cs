using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Commands;

public record CreateStudentCommand(Student Student) : IRequest<int>;