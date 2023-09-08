using Application.Dto.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Abstractions
{
    public interface IDepartmentValidator
    {
        public void Validate(CreateDepartmentDto department);
        public void Validate(UpdateDepartmentDto department);
        public void Validate(int DepartmentId);
    }
}
