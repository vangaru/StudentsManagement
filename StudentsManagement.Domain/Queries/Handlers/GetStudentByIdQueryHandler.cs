using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Queries.Handlers;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Student>
{
    private readonly IRepository<Student> _studentsRepository;

    public GetStudentByIdQueryHandler(IRepository<Student> studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _studentsRepository.GetEntityWithIncludeAsync(student => student.Id == request.StudentId,
            cancellationToken,
            student => student.AssignedCourses);
    }
}