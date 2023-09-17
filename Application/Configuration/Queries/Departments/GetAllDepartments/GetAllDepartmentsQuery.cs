using Application.Dto.Student;
using MediatR;

namespace Application.Configuration.Queries.Departments.GetAllDepartments;

public record GetAllDepartmentsQuery() : IRequest<ListDepartmentsDto> { }