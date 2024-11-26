namespace ClubMember.Data;

public interface IRegister
{
    bool Register(string[] fields);
    bool EmailExists(string emailAddress);
}