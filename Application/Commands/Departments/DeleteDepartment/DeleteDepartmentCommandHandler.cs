using Core.Repositories;
using MediatR;

namespace Application.Commands.Departments.DeleteDepartment;

public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;
    
    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    
    public Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = _departmentRepository.GetById(request.Id);
        if (department is null)
        {
            throw new Exception($"Department with ID {request.Id} does not exist.");
        }
        
        _departmentRepository.Delete(department);
        return Task.CompletedTask;
    }
}