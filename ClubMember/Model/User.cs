using System.ComponentModel.DataAnnotations.Schema;

namespace ClubMember.Model;

public class User
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string ZipCode { get; set; }

}

