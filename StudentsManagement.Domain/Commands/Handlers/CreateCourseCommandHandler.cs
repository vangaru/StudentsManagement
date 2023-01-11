using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly IRepository<Student> _studentsRepository;
    private readonly IRepository<Course> _coursesRepository;

    public CreateCourseCommandHandler(IRepository<Course> coursesRepository, IRepository<Student> studentsRepository)
    {
        _coursesRepository = coursesRepository;
        _studentsRepository = studentsRepository;
    }

    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IEnumerable<Student> assignedStudents = await _studentsRepository
            .GetEntitiesAsync(student => request.AssignedStudentsIds.Contains(student.Id), cancellationToken);
        
        var course = new Course
        {
            Name = request.CourseName,
            Description = request.Description,
            AssignedStudents = assignedStudents.ToList()
        };

        return await _coursesRepository.CreateEntityAsync(course, cancellationToken);
    }
}