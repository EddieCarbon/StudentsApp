using AutoMapper;
using Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Commands.Departments.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<UpdateDepartmentCommand> _validator;
    private readonly IMapper _mapper;
    
    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IValidator<UpdateDepartmentCommand> validator, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid) 
        {
            var errorList = result.Errors.Select(x => x.ErrorMessage);
            throw new ValidationException($"Invalid command, reasons: {string.Join(",", errorList.ToArray())}");
        }
        
        var existingDepartment = _departmentRepository.GetById(request.DepartmentId);
        if (existingDepartment == null)
        {
            throw new Exception($"Department with ID {request.DepartmentId} does not exist.");
        }
        
        var department = _mapper.Map(request, existingDepartment);
        
        _departmentRepository.Update(department);
    }
}