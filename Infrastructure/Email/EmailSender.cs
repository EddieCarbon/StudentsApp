using Core.Abstraction;

namespace Infrastructure.Email;

public class EmailSender : IEmailSender
{
    public bool SendEmailPasswordReset(string email, string resetLink)
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktop, email + ".txt");

        try
        {
            File.WriteAllText(filePath, resetLink);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
}

