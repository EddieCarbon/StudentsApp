using Application.Dto.Student;
using MediatR;

namespace Application.Configuration.Queries.Departments.GetDepartmentById;

public record GetDepartmentByIdQuery(int Id) : IRequest<DepartmentDetailDto> { }