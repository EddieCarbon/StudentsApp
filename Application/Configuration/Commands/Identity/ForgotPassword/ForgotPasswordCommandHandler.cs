using Application.Identity;
using Core.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Configuration.Commands.Identity.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;
    
    public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender, IConfiguration configuration)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return;
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = $"{_configuration["SiteURL"]}/reset-password?token={token}&email={request.Email}";
        _emailSender.SendEmailPasswordReset(request.Email, callbackUrl);
    }
}