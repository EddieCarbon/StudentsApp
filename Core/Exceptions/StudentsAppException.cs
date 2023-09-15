using System.Net;


namespace Core.Exceptions
{
    public abstract class StudentsAppException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        public StudentsAppException(string messege) : base(messege)
        {

        }
    }
}
