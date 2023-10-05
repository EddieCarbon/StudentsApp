﻿using Application.Dto.Student;
using Application.Dto.StudentAddress;
using MediatR;

namespace Application.Configuration.Commands.Students.CreateStudent
{
    public class CreateStudentCommand(string userId) : IRequest<StudentDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int StartingStudyYear { get; set; }

        public CreateStudentAddress CreateStudentAddress { get; set; }
        public int CurrentDepartmentId { get; set; }
    }
}
