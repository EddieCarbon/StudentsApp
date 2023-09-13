using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Students.DeleteStudent;

internal class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ILogger<DeleteStudentCommandHandler> _logger;
    
    public DeleteStudentCommandHandler(IStudentRepository studentRepository, ILogger<DeleteStudentCommandHandler> logger)
    {
        _studentRepository = studentRepository;
        _logger = logger;
    }

    public Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = _studentRepository.GetById(request.Id);
        if (student is null)
        {
            throw new Exception($"Student with ID {request.Id} does not exist.");
        }
        
        _studentRepository.Delete(student);
        
        _logger.LogDebug($"Student with ID {request.Id} was deleted.");
        
        return Task.CompletedTask;
    }
}