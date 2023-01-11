using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
{
    private readonly IRepository<Course> _coursesRepository;

    public UpdateCourseCommandHandler(IRepository<Course> coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public Task<int> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _coursesRepository.UpdateEntityAsync(request.Course, cancellationToken);
    }
}