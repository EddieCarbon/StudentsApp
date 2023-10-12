using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using MediatR;

namespace Application.Configuration.Queries.Students.GetAllStudents;

internal class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, ListStudentsDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetAllStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public Task<ListStudentsDto> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = _studentRepository.GetAll();

        var studentDto = _mapper.Map<IEnumerable<Student>, ListStudentsDto>(students);

        return Task.FromResult(studentDto);
    }
}