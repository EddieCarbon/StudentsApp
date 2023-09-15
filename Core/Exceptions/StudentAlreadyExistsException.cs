
using System.Net;


namespace Core.Exceptions
{
    public class StudentAlreadyExistsException : StudentsAppException
    {
        public string Email { get; set; }

        public StudentAlreadyExistsException(string email) : base($"Student with email {email} already exists.")
            => Email = email;

        public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    }
}
