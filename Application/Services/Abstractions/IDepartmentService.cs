using Application.Dto.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstractions
{
    public interface IDepartmentService
    {
        ListDepartmentsDto GetAllDepartments();
        DepartmentDetailDto GetDepartmentById(int DepartmentId);
        DepartmentDto CreateDepartment(CreateDepartmentDto newDepartment);
        void UpdateDepartment(UpdateDepartmentDto updateDepartment);
        void DeleteDepartment(int DepartmentId);
    }
}
