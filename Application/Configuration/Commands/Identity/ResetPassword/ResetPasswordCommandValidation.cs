using Application.Identity.Classes;
using Application.Identity.Enums;
using FluentValidation;

namespace Application.Configuration.Commands.Identity.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Incorrect email format.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        RuleFor(x => x).Must(x => PasswordManager.CheckPasswordStrength(x.Password) != PasswordScore.VeryStrong).WithMessage("Password is not very strong.").WithName("Password");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required.");
        RuleFor(x => x).Must(x => ComparePasswords(x.Password, x.ConfirmPassword)).WithMessage("Passwords are not equal.").WithName("ConfirmPassword");
    }

    private bool ComparePasswords(string password, string confirmPassword)
    {
        if (password.Equals(confirmPassword))
            return true;
        else
            return false;
    }
}