using Application.Dto.Student;
using AutoMapper;
using Core.Repositories;
using MediatR;

namespace Application.Queries.Students.GetAllStudents;

internal class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, ListStudentsDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    
    public GetAllStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    
    public async Task<ListStudentsDto> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = _studentRepository.GetAll();
        return _mapper.Map<ListStudentsDto>(students);
    }
}