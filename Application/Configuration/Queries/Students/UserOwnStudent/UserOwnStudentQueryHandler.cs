using System.Threading;
using System.Threading.Tasks;
using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using MediatR;

namespace Application.Configuration.Queries.Students.GetStudentById;

internal class UserOwnStudentQueryHandler : IRequestHandler<UserOwnStudentQuery, bool>
{
    private readonly IStudentRepository _studentRepository;

    public UserOwnStudentQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    
    public async Task<bool> Handle(UserOwnStudentQuery request, CancellationToken cancellationToken)
    {
        var student = _studentRepository.GetById(request.studentId);

        if (student == null)
        {
            
            return false;
        }

        if (student.UserId != request.userId)
        {
            return false;
        }

        return true;
    }

}