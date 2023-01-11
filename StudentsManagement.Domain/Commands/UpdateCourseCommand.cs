using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Commands;

public record UpdateCourseCommand(Course Course) : IRequest<int>;