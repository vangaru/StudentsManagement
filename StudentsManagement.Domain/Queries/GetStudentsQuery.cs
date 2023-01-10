using MediatR;
using StudentsManagement.Domain.Models;

namespace StudentsManagement.Domain.Queries;

public class GetStudentsQuery : IRequest<IEnumerable<Student>>
{
}