namespace AdminAPI;

public interface IEmployee
{
    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    decimal Salary { get; set; }
}