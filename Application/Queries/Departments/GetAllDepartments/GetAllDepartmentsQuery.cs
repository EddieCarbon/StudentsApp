using Application.Dto.Student;
using MediatR;

namespace Application.Queries.Departments.GetAllDepartments;

public record GetAllDepartmentsQuery() : IRequest<ListDepartmentsDto> { }