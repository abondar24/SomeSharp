namespace SurvilanceManager.Models;

public interface IEmployee
{
    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string JobTitle { get; set; }
}