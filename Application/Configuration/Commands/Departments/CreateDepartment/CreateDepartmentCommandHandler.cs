using System.Threading;
using System.Threading.Tasks;
using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Configuration.Commands.Departments.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateDepartmentCommandHandler> _logger;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<CreateDepartmentCommandHandler> logger)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    { 
        var department = _mapper.Map<Department>(request);

        _departmentRepository.Add(department);

        _logger.LogDebug($"Create departmetnt with ID  {department.DepartmentId}");

        return _mapper.Map<DepartmentDto>(department);
    }
}