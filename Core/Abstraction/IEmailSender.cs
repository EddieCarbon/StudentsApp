namespace Core.Abstraction;

public interface IEmailSender
{
    bool SendEmailPasswordReset(string email, string callbackUrl);
}