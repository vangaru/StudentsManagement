using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, int>
{
    private readonly IRepository<Student> _studentsRepository;
    private readonly IRepository<Course> _coursesRepository;

    public UpdateStudentCommandHandler(IRepository<Student> studentsRepository, IRepository<Course> coursesRepository)
    {
        _studentsRepository = studentsRepository;
        _coursesRepository = coursesRepository;
    }

    public async Task<int> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IEnumerable<Course> assignedCourses =
            await _coursesRepository.GetEntitiesAsync(course => request.AssignedCoursesIds.Contains(course.Id), 
                cancellationToken);

        Student student = await _studentsRepository.GetEntityWithIncludeAsync(
            student => student.Id == request.StudentId,
            cancellationToken, student => student.AssignedCourses);

        student.Name = request.StudentName;
        student.AssignedCourses.Clear();
        student.AssignedCourses.AddRange(assignedCourses);
        
        return await _studentsRepository.UpdateEntityAsync(student, cancellationToken);
    }
}