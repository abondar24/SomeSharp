using AdminAPI;

namespace SchoolAdmin.Models;

public class DeputyHeadMaster : EmployeeBase
{
    public override decimal Salary { get => base.Salary + (base.Salary * 0.75m); }
}