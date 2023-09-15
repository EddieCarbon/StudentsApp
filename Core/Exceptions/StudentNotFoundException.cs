using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class StudentNotFoundException : StudentsAppException
    {
        public int Id { get; set; }
        public StudentNotFoundException(int id) : base($"Student with ID {id} was not found.")
            => Id = id;

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    }
}
