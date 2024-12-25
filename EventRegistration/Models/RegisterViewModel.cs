using System.ComponentModel.DataAnnotations;


namespace EventRegistration.Models;

public class RegisterViewModel : BaseLoginRegisterViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Role { get; set; }
}
