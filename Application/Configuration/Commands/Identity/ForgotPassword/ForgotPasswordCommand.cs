using MediatR;

namespace Application.Configuration.Commands.Identity.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest;