using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Configuration.Commands.Students.UpdateStudent;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateStudentCommandHandler> _logger;

    public UpdateStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper, ILogger<UpdateStudentCommandHandler> logger)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var existingStudent = _studentRepository.GetById(request.Id);
        if (existingStudent == null)
        {
            throw new Exception($"Student with ID {request.Id} does not exist.");
        }

        var student = _mapper.Map(request, existingStudent);

        _studentRepository.Update(student);

        _logger.LogDebug($"Student with ID {student.Id} was updated.");
    }
}