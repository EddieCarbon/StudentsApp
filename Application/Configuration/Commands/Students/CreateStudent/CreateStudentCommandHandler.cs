using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Configuration.Commands.Students.CreateStudent
{
    internal class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStudentCommandHandler> _logger;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper, ILogger<CreateStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);

            _studentRepository.Add(student);

            _logger.LogDebug($"Student with ID {student.Id} was created.");

            return _mapper.Map<StudentDto>(student);
        }
    }
}
