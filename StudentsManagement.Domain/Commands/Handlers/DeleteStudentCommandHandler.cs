using MediatR;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;

namespace StudentsManagement.Domain.Commands.Handlers;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, int>
{
    private readonly IRepository<Student> _studentsRepository;

    public DeleteStudentCommandHandler(IRepository<Student> studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _studentsRepository.DeleteEntityAsync(request.StudentId, cancellationToken);
    }
}