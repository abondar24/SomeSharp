using System.ComponentModel.DataAnnotations;

namespace EventRegistration.Models;

public class BaseLoginRegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}