using Application.Dto.Student;
using MediatR;

namespace Application.Configuration.Commands.Departments.CreateDepartment;

public class CreateDepartmentCommand : IRequest<DepartmentDto>
{
    public string DepartmentName { get; set; }
    public string BuildingNumber { get; set; }
}