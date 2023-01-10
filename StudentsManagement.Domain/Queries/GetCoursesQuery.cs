using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Queries;

public class GetCoursesQuery : IRequest<IEnumerable<Course>>
{
}