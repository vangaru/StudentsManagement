using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Queries.Handlers;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<Course>>
{
    private readonly IRepository<Course> _coursesRepository;

    public GetCoursesQueryHandler(IRepository<Course> coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public Task<IEnumerable<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _coursesRepository.GetEntitiesAsync();
    }
}