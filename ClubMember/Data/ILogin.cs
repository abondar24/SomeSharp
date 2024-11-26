using ClubMember.Models;

namespace ClubMember.Data;

public interface ILogin
{
    User Login(string emailAddress, string password);
}