using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Student;
using FluentValidation;
using Application.Validators;

namespace Application.Commands.Students.CreateStudent
{
    public class CreateStudentCommandValidation : AbstractValidator<CreateStudentCommand>
    {

        public CreateStudentCommandValidation()
        {
            
        }
    }
}
