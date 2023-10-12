using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Configuration.Commands.Departments.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateDepartmentCommandHandler> _logger;

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<UpdateDepartmentCommandHandler> logger)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var existingDepartment = _departmentRepository.GetById(request.DepartmentId);
        if (existingDepartment == null)
        {
            throw new Exception($"Department with ID {request.DepartmentId} does not exist.");
        }

        var department = _mapper.Map(request, existingDepartment);

        _logger.LogDebug($"Update departmetnt with ID  {department.DepartmentId}");

        _departmentRepository.Update(department);
    }
}