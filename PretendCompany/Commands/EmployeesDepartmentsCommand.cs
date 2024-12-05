using PretendCompany.Commands;

namespace PretendCompany.Commands;

public class EmployeesDepartmentsCommand : BaseQueryCommand
{
    public override void Execute()
    {
        var employeesInDepartments = from dept in _departments
                                     join emp in _employees
                                     on dept.Id equals emp.DepartmentId
                                     orderby emp.DepartmentId, emp.Salary descending
                                     select new
                                     {
                                         FullName = emp.FirstName + " " + emp.LastName,
                                         Salary = emp.Salary,
                                         DepartmentName = dept.LongName
                                     };

        Console.WriteLine("Employees in departments");
        foreach (var emp in employeesInDepartments)
        {
            Console.WriteLine($"{emp.FullName,-20}{emp.Salary,10}\t{emp.DepartmentName}");
        }

    }

    public override void Execute(string queryParam)
    {
        throw new NotImplementedException();
    }
}