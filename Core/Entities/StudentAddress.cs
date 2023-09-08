using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StudentAddress
    {
        public int StudentAddressId { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
