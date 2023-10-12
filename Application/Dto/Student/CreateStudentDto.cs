using System;
using Application.Dto.StudentAddress;

namespace Application.Dto.Student;

public class CreateStudentDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int StartingStudyYear { get; set; }

    public CreateStudentAddress CreateStudentAddress { get; set; }
    public int CurrentDepartmentId { get; set; }
    /*public CreateStudentCourse CreateStudentCourse { get; set; }*/
}
