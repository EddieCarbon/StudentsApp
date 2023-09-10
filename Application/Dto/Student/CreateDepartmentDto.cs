using MediatR;

namespace Application.Dto.Student
{
    public class CreateDepartmentDto : IRequest<DepartmentDto>
    {
        public string DepartmentName { get; set; }
        public string BuildingNumber { get; set; }
    }
}
