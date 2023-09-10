using Application.Dto.Student;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Queries.Departments.GetDepartmentById;

public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDetailDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    
    public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }
    
    public Task<DepartmentDetailDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = _departmentRepository.GetById(request.Id);
        
        var departmentDetailDto = _mapper.Map<DepartmentDetailDto>(department);
        
        return Task.FromResult(departmentDetailDto);
    }
}