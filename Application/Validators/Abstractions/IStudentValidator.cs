using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Dto.Student;

namespace Application.Validators.Abstractions;

public interface IStudentValidator
{
    public void Validate(CreateStudentDto student);
    public void Validate(UpdateStudentDto student);
    public void Validate(int Id);
}
