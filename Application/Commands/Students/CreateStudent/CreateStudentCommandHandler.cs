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
            ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid) 
            {
                var errorList = result.Errors.Select(x => x.ErrorMessage);
                throw new ValidationException($"Invalid command, reasons: {string.Join(",", errorList.ToArray())}");
            }
            
            var student = _mapper.Map<Student>(request);

            _studentRepository.Add(student);
            
            // TODO: Check if this is ok 
            return _mapper.Map<StudentDto>(student);
        }
    }
}
