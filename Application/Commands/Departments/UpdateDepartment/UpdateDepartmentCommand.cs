using MediatR;

namespace Application.Commands.Departments.UpdateDepartment;

public class UpdateDepartmentCommand : IRequest
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public string BuildingNumber { get; set; }
}