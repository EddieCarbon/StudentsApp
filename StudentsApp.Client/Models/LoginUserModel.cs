using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentsApp.Client;

public class LoginUserModel
{
    [Required(ErrorMessage = "Name is required.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}