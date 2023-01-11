using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Queries;

public record GetCourseByIdQuery(Guid CourseId) : IRequest<Course>;