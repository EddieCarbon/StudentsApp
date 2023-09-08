using Application.Dto.Student;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Commands.Students.CreateStudent
{
    internal class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateStudentCommand> _validator;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper, IValidator<CreateStudentCommand> validator)
        {
            _studentRepository = studentRepository;
            _mapper = mapper; 
            _validator = validator;
        }
        
        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var student = _mapper.Map<Student>(request);

            _studentRepository.Add(student);

            return _mapper.Map<StudentDto>(student);
        }

        //public StudentDto CreateStudent(CreateStudentDto newStudent)
        //{
        //    _studentValidator.Validate(newStudent);

        //    var student = _mapper.Map<Student>(newStudent);

        //    _studentRepository.Add(student);

        //    return _mapper.Map<StudentDto>(student);
        //}
    }
}
