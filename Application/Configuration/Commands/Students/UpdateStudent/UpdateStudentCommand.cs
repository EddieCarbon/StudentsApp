using System;
using Application.Dto.StudentAddress;
using Application.Dto.StudentDepartment;
using MediatR;

namespace Application.Configuration.Commands.Students.UpdateStudent;

public record UpdateStudentCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int StartingStudyYear { get; set; }

    public CreateStudentAddress CreateStudentAddress { get; set; }
    public CreateStudentDepartment CreateStudentDepartment { get; set; }
}