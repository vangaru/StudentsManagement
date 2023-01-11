using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IRepository<Student> _studentsRepository;

    public CreateStudentCommandHandler(IRepository<Student> studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _studentsRepository.CreateEntityAsync(request.Student, cancellationToken);
    }
}