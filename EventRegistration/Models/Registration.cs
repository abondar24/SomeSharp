using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class Registration
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public int EventId { get; set; }


    public string UserId { get; set; }

}