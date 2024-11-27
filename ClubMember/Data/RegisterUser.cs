using ClubMember.FieldValidators;
using ClubMember.Models;

namespace ClubMember.Data;


public class RegisterUser : IRegister
{
    public bool Register(string[] fields)
    {
        using var dbContext = new ClubMemberDbContext();

        var dateFormat = "dd/MM/yyyy";  // EU format (day/month/year)
        var dateProvider = new System.Globalization.CultureInfo("en-GB");
        User user = new()
        {
            Email = fields[(int)UserRegistrationField.Email],
            FirstName = fields[(int)UserRegistrationField.FirstName],
            LastName = fields[(int)UserRegistrationField.LastName],
            Password = fields[(int)UserRegistrationField.Password],
            DateOfBirth = DateTime.ParseExact(fields[(int)UserRegistrationField.DateOfBirth], dateFormat, dateProvider),
            PhoneNumber = fields[(int)UserRegistrationField.PhoneNumber],
            Street = fields[(int)UserRegistrationField.Street],
            City = fields[(int)UserRegistrationField.City],
            ZipCode = fields[(int)UserRegistrationField.ZipCode]
        };


        dbContext.Users.Add(user);
        dbContext.SaveChanges();

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