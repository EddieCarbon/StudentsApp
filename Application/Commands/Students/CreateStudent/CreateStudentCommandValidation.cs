using FluentValidation;

namespace Application.Commands.Students.CreateStudent
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
                .NotEmpty().WithMessage("Email name is required.");
            
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.");
            
            RuleFor(x => x.StartingStudyYear)
                .NotEmpty().WithMessage("Year enrolled name is required.");
            
        }
    }
}
