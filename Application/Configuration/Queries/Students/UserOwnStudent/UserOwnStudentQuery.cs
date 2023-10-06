using Application.Dto.Student;
using MediatR;

namespace Application.Configuration.Queries.Students.GetStudentById;

public record UserOwnStudentQuery(int studentId, string userId) : IRequest<bool> { }