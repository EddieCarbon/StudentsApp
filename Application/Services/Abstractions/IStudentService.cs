using Application.Dto.Student;
using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstractions;

public interface IStudentService
{
    ListStudentsDto GetAllStudents();
    StudentDetailDto GetStudentById(int id);
    StudentDetailDto GetStudentByEmail(string Email);
    StudentDto CreateStudent(CreateStudentDto newProduct);
    void UpdateStudent(UpdateStudentDto updateProduct);
    void DeleteStudent(int id);
}
