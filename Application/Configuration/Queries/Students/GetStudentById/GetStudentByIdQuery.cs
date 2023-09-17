using Application.Dto.Student;
using MediatR;

namespace Application.Configuration.Queries.Students.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<StudentDetailDto> { }