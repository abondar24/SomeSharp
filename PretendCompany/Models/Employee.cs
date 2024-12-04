namespace PretendCompany.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public decimal Salary { get; set; }

    public bool IsManager { get; set; }

    public int DepartmentId { get; set; }
}