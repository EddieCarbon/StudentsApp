using Application.Dto.Student;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Students.GetStudentByEmail;

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
        var student = await _studentRepository.GetAll().SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        
        var studentDto = _mapper.Map<StudentDetailDto>(student);
        
        return studentDto;
    }
}