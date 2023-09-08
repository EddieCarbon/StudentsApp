using Application.Dto.Student;
using MediatR;

namespace Application.Commands.Students.CreateStudent
{
    public record CreateStudentCommand : IRequest<StudentDto>
    {
        public string Name { get; set; }
    }
}
