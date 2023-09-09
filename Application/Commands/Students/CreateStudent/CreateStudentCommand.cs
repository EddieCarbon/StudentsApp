using Application.Dto.Student;
using MediatR;

namespace Application.Commands.Students.CreateStudent
{
    public record CreateStudentCommand : IRequest<StudentDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int StartingStudyYear { get; set; }
    }
}
