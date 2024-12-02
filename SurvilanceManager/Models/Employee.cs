namespace SurvilanceManager.Models;

public class Employee : IEmployee
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string JobTitle { get; set; }
}