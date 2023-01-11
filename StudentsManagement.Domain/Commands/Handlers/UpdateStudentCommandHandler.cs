using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, int>
{
    private readonly IRepository<Student> _studentsRepository;

    public UpdateStudentCommandHandler(IRepository<Student> studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public Task<int> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _studentsRepository.UpdateEntityAsync(request.Student, cancellationToken);
    }
}