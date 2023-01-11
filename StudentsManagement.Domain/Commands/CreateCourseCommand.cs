using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Commands;

public record CreateCourseCommand(Course Course) : IRequest<int>;