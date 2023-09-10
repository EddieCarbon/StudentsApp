using FluentValidation;

namespace Application.Commands.Departments.CreateDepartment;

public class CreateDepartmentCommandValidation : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidation()
    {
        RuleFor(x => x.DepartmentName)
            .NotEmpty().WithMessage("Department name is required");
        
        RuleFor(x => x.BuildingNumber)
            .NotEmpty().WithMessage("Building number is required");
    }
}