using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Queries.Handlers;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IEnumerable<Student>>
{
    private readonly IRepository<Student> _studentsRepository;

    public GetStudentsQueryHandler(IRepository<Student> studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public Task<IEnumerable<Student>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _studentsRepository.GetEntitiesAsync();
    }
}