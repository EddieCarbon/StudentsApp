using System.Threading;
using System.Threading.Tasks;
using Application.Dto.Student;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Configuration.Queries.Departments.GetAllDepartments;

public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, ListDepartmentsDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public Task<ListDepartmentsDto> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = _departmentRepository.GetAll();

        var listDepartmentsDto = _mapper.Map<ListDepartmentsDto>(departments);

        return Task.FromResult(listDepartmentsDto);
    }
}