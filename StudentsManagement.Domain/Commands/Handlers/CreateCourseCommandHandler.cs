using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly IRepository<Course> _coursesRepository;

    public CreateCourseCommandHandler(IRepository<Course> coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _coursesRepository.CreateEntityAsync(request.Course, cancellationToken);
    }
}