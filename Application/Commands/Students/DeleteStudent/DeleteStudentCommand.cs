using MediatR;

namespace Application.Commands.Students.DeleteStudent;

public record DeleteStudentCommand(int Id) : IRequest
{
};