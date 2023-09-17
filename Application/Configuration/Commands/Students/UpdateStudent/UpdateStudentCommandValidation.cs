using FluentValidation;

namespace Application.Configuration.Commands.Students.UpdateStudent;

public class UpdateStudentCommandValidation : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(20).WithMessage("First name cannot be longer than 20 characters.");

        RuleFor(x => x.Surname)
           .NotEmpty().WithMessage("Last name is required.")
           .MaximumLength(40).WithMessage("Last name cannot be longer than 40 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email name is required.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.");

        RuleFor(x => x.StartingStudyYear)
           .NotEmpty().WithMessage("Year enrolled name is required.");
    }

}