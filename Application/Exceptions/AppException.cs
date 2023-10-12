using System;
using System.Net;

namespace Application.Exceptions;

public abstract class AppException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    public AppException(string message) : base(message)
    {

    }
}
