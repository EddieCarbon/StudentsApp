using System;
using FluentValidation;

namespace Application.Configuration.Commands.Students.CreateStudent
{
    public class CreateStudentCommandValidation : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Student can not have an empty name.")
                .MaximumLength(20).WithMessage("Student name can be 20 characters at most.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname can not have an empty name.")
                .MaximumLength(40).WithMessage("Surname name can be 40 characters at most.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email name is required.")
                .MaximumLength(100).WithMessage("Last name cannot be longer than 50 characters.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of birth cannot be in the future.")
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Student must be at least 18 years old.");

            RuleFor(x => x.StartingStudyYear)
                .NotEmpty().WithMessage("Year enrolled name is required.");

        }
    }
}
