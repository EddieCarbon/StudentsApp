using FluentValidation;
using Application.Validators;
using Application.Validators.Abstractions;

namespace Application.Commands.Students.CreateStudent
{
    public class CreateStudentCommandValidation : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidation(IStudentValidator studentValidator)
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
            // RuleFor(x => x.Surname).NotEmpty().MaximumLength(20);
            // RuleFor(x => x.Email).NotEmpty().EmailAddress();
            // RuleFor(x => x.DateOfBirth).NotEmpty().LessThan(DateTime.Now);
            // RuleFor(x => x.StartingStudyYear).NotEmpty().LessThan(DateTime.Now.Year);
        }
    }
}
