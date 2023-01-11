using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
{
    private readonly IRepository<Course> _coursesRepository;

    public DeleteCourseCommandHandler(IRepository<Course> coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _coursesRepository.DeleteEntityAsync(request.CourseId, cancellationToken);
    }
}