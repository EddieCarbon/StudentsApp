using FluentValidation;

namespace Application.Commands.Departments.UpdateDepartment;

public class UpdateDepartmentCommandValidation : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidation()
    {
        RuleFor(x => x.DepartmentId)
            .NotEmpty();
        
        RuleFor(x => x.DepartmentName)
            .NotEmpty();
        
        RuleFor(x => x.BuildingNumber)
            .NotEmpty();
    }
}