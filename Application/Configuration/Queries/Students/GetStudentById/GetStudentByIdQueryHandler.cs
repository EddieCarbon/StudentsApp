using Application.Dto.Student;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Configuration.Queries.Students.GetStudentById;

internal class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public Task<StudentDetailDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = _studentRepository.GetById(request.Id);

        var studentDetailDto = _mapper.Map<StudentDetailDto>(student);

        return Task.FromResult(studentDetailDto);
    }
}