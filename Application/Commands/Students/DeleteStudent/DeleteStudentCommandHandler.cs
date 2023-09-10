using Core.Repositories;
using MediatR;

namespace Application.Commands.Students.DeleteStudent;

internal class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    
    public DeleteStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = _studentRepository.GetById(request.Id);
        if (student is null)
        {
            throw new Exception($"Student with ID {request.Id} does not exist.");
        }
        
        _studentRepository.Delete(student);
        return Task.CompletedTask;
    }
}