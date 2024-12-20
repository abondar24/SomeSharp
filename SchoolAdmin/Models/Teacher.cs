
using AdminAPI;

namespace SchoolAdmin.Models;

public class Teacher : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.05m); }
}
