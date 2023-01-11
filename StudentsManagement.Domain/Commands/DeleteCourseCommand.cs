using MediatR;

namespace StudentsManagement.Domain.Commands;

public record DeleteCourseCommand(Guid CourseId) : IRequest<int>;