using Application.Dto.Student;
using MediatR;

namespace Application.Queries.Students.GetStudentByEmail;

public record GetStudentByEmailQuery(string Email) : IRequest<StudentDetailDto> { }