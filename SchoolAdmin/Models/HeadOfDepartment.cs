using AdminAPI;

namespace SchoolAdmin.Models;
public class HeadOfDeparment : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.13m); }
}