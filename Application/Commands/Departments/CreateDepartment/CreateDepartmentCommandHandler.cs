using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Commands.Departments.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<CreateDepartmentCommand> _validator;
    private readonly IMapper _mapper;
    
    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IValidator<CreateDepartmentCommand> validator, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid) 
        {
            var errorList = result.Errors.Select(x => x.ErrorMessage);
            throw new ValidationException($"Invalid command, reasons: {string.Join(",", errorList.ToArray())}");
        }
        
        var department = _mapper.Map<Department>(request);

        _departmentRepository.Add(department);
        
        return _mapper.Map<DepartmentDto>(department);
    }
}