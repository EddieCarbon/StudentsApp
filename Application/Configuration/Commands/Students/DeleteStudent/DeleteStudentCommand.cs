using MediatR;

namespace Application.Configuration.Commands.Students.DeleteStudent;

public record DeleteStudentCommand(int Id) : IRequest
{
};