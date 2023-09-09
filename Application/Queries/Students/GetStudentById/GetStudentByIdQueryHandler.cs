using Application.Dto.Student;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Queries.Students.GetStudentById;

internal class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    
    public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    
    public async Task<StudentDetailDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = _studentRepository.GetById(request.Id);
        return _mapper.Map<StudentDetailDto>(student);
    }
}