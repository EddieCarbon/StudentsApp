using Application.Dto.Student;
using MediatR;

namespace Application.Queries.Departments.GetDepartmentById;

public record GetDepartmentByIdQuery(int Id) : IRequest<DepartmentDetailDto> { }