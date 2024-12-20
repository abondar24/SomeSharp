using AdminAPI;

namespace SchoolAdmin.Models;

public class HeadMaster : EmployeeBase
{
    public override decimal Salary { get => base.Salary * 5; }
}