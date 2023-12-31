using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto.Student;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Configuration.Queries.Students.GetStudentByEmail;

internal class GetStudentByEmailQueryHandler : IRequestHandler<GetStudentByEmailQuery, StudentDetailDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentByEmailQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<StudentDetailDto> Handle(GetStudentByEmailQuery request, CancellationToken cancellationToken)
    {
        var student = _studentRepository.GetAll().SingleOrDefault(x => x.Email == request.Email);

        var studentDto = _mapper.Map<StudentDetailDto>(student);

        return studentDto;
    }
}