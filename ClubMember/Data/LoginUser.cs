using ClubMember.Models;

namespace ClubMember.Data;


public class LoginUser : ILogin
{
    public User Login(string emailAddress, string password)
    {

        using var dbContext = new ClubMemberDbContext();

        var user = dbContext.Users.FirstOrDefault(u => string.Equals(u.Email.Trim().ToLower(), emailAddress.Trim().ToLower()) &&
            u.Password.Equals(password));

        return user;
    }
}