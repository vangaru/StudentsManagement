using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Queries.Handlers;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Course>
{
    private readonly IRepository<Course> _coursesRepository;

    public GetCourseByIdQueryHandler(IRepository<Course> coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public Task<Course> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _coursesRepository.GetEntityWithIncludeAsync(course => course.Id == request.CourseId, 
            cancellationToken,
            course => course.AssignedStudents);
    }
}