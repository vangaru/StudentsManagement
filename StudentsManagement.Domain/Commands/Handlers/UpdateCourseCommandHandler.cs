using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, int>
{
    private readonly IRepository<Course> _coursesRepository;
    private readonly IRepository<Student> _studentsRepository;

    public UpdateCourseCommandHandler(IRepository<Course> coursesRepository, IRepository<Student> studentsRepository)
    {
        _coursesRepository = coursesRepository;
        _studentsRepository = studentsRepository;
    }

    public async Task<int> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IEnumerable<Student> assignedStudents =
            await _studentsRepository.GetEntitiesAsync(student => request.AssignedStudentsIds.Contains(student.Id),
                cancellationToken);

        Course course = await _coursesRepository.GetEntityWithIncludeAsync(course => course.Id == request.CourseId,
            cancellationToken, course => course.AssignedStudents);

        course.Name = request.CourseName;
        course.Description = request.CourseDescription;
        course.AssignedStudents.Clear();
        course.AssignedStudents.AddRange(assignedStudents);
        
        return await _coursesRepository.UpdateEntityAsync(course, cancellationToken);
    }
}