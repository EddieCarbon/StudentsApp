using Application.Dto.Student;
using MediatR;

namespace Application.Queries.Students.GetAllStudents;

public record GetAllStudentsQuery() : IRequest<ListStudentsDto> { }