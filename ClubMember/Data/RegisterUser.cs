using ClubMember.FieldValidators;
using ClubMember.Models;

namespace ClubMember.Data;


public class RegisterUser : IRegister
{
    public bool Register(string[] fields)
    {
        using var dbContext = new ClubMemberDbContext();

        User user = new()
        {
            Email = fields[(int)UserRegistrationField.Email],
            FirstName = fields[(int)UserRegistrationField.FirstName],
            LastName = fields[(int)UserRegistrationField.LastName],
            Password = fields[(int)UserRegistrationField.Password],
            DateOfBirth = DateTime.Parse(fields[(int)UserRegistrationField.DateOfBirth]),
            PhoneNumber = fields[(int)UserRegistrationField.PhoneNumber],
            Street = fields[(int)UserRegistrationField.Street],
            City = fields[(int)UserRegistrationField.City],
            ZipCode = fields[(int)UserRegistrationField.ZipCode]
        };


        dbContext.Users.Add(user);

        return true;
    }


    public bool EmailExists(string emailAddress)
    {
        var emailExists = false;

        using var dbCOntext = new ClubMemberDbContext();

        emailExists = dbCOntext.Users.Any(u => string.Equals(u.Email.Trim().ToLower(), emailAddress.Trim().ToLower()));

        return emailExists;
    }
}