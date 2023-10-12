using System.ComponentModel.DataAnnotations;

namespace StudentsApp.Client;

public class RegisterUserModel
{
    [Required(ErrorMessage = "Name is required.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}