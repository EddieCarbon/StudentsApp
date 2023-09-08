using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Student
{
    public class ListDepartmentsDto
    {
        public int Count { get; set; }
        public IEnumerable<DepartmentDto> Departments { get; set; }
    }
}
