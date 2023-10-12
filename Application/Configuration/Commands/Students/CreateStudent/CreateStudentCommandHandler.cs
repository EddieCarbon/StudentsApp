using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Configuration.Commands.Students.CreateStudent
{
    internal class CreateStudentCommandHandler() : IRequestHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStudentCommandHandler> _logger;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper, ILogger<CreateStudentCommandHandler> logger) : this()
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            bool isAlreadyExist = await _studentRepository.IsAlreadyExistAsync(request.Email, cancellationToken);
            if (isAlreadyExist)
            {
                throw new Exception("Student already exists.");
            }

            var student = _mapper.Map<Student>(request);

            _studentRepository.Add(student);

            _logger.LogDebug($"Student with ID {student.Id} was created.");

            return _mapper.Map<StudentDto>(student);
        }
    }
}
