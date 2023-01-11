using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Queries;

public record GetStudentByIdQuery(Guid StudentId) : IRequest<Student>;