using Application.Dto.Student;
using MediatR;

namespace Application.Configuration.Queries.Students.GetAllStudents;

public record GetAllStudentsQuery() : IRequest<ListStudentsDto> { }