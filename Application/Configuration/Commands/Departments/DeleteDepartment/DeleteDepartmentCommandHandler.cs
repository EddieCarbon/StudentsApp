using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Configuration.Commands.Departments.DeleteDepartment;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILogger<DeleteDepartmentCommandHandler> _logger;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, ILogger<DeleteDepartmentCommandHandler> logger)
    {
        _departmentRepository = departmentRepository;
        _logger = logger;
    }

    public Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = _departmentRepository.GetById(request.Id);
        if (department is null)
        {
            throw new Exception($"Department with ID {request.Id} does not exist.");
        }

        _departmentRepository.Delete(department);

        _logger.LogDebug("Department with ID {Id} was deleted.", request.Id);

        return Task.CompletedTask;
    }

    
}