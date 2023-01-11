using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IRepository<Student> _studentsRepository;
    private readonly IRepository<Course> _coursesRepository;

    public CreateStudentCommandHandler(IRepository<Student> studentsRepository, IRepository<Course> coursesRepository)
    {
        _studentsRepository = studentsRepository;
        _coursesRepository = coursesRepository;
    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IEnumerable<Course> assignedCourses = await _coursesRepository
            .GetEntitiesAsync(course => request.AssignedCoursesIds.Contains(course.Id), cancellationToken);
        
        var student = new Student
        {
            Name = request.StudentName,
            AssignedCourses = assignedCourses.ToList()
        };
        
        return await _studentsRepository.CreateEntityAsync(student, cancellationToken);
    }
}