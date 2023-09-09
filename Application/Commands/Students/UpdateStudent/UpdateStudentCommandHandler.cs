using AutoMapper;
using Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;


namespace Application.Commands.Students.UpdateStudent;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IValidator<UpdateStudentCommand> _validator;
    private readonly IMapper _mapper;
    
    public UpdateStudentCommandHandler(IStudentRepository studentRepository, IValidator<UpdateStudentCommand> validator, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        ValidationResult result = await _validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid) 
        {
            var errorList = result.Errors.Select(x => x.ErrorMessage);
            throw new ValidationException($"Invalid command, reasons: {string.Join(",", errorList.ToArray())}");
        }
        
        var existingStudent = _studentRepository.GetById(request.Id);
        if (existingStudent == null)
        {
            throw new Exception($"Student with ID {request.Id} does not exist.");
        }
        
        var student = _mapper.Map(request, existingStudent);
        
        _studentRepository.Update(student);
    }
}