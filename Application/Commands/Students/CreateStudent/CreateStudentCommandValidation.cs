using FluentValidation;
using Application.Validators.Abstractions;

namespace Application.Commands.Students.CreateStudent
{
    public class CreateStudentCommandValidation : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        }
    }
}
