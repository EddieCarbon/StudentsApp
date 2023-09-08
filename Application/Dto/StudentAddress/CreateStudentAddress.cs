using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.StudentAddress
{
    public class CreateStudentAddress
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
    }
}
