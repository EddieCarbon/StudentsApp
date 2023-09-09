using MediatR;

namespace Application.Commands.Students.UpdateStudent;

public record UpdateStudentCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int StartingStudyYear { get; set; }
}