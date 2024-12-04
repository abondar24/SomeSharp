using PretendCompany.Extensions;
using PretendCompany.Models;

namespace PretendCompany;

class Program
{
    static void Main(string[] args)
    {
        var employees = Data.GetEmployees();
        var departments = Data.GetDepartments();

        //TODO: refactor to commands
        var managers = employees.Filter(emp => emp.IsManager);
        PrintFilteredEmployees(managers);

        var hrDepartments = departments.Filter(dpt => dpt.ShortName.Equals("HR"));
        PrintFilteredDepartments(hrDepartments);

        var employeesInDepartment = from emp in employees
                                    join dept in departments
                                    on emp.DepartmentId equals dept.Id
                                    select new
                                    {
                                        FirstName = emp.FirstName,
                                        LastName = emp.LastName,
                                        Salary = emp.Salary,
                                        Manager = emp.IsManager,
                                        Department = dept.LongName
                                    };
        Console.WriteLine($"Average salary: {employeesInDepartment.Average(emp => emp.Salary)}");


    }


    private static void PrintFilteredEmployees(List<Employee> employees)
    {
        foreach (var emp in employees)
        {
            Console.WriteLine($"First Name: {emp.FirstName}");
            Console.WriteLine($"Last Name: {emp.LastName}");
            Console.WriteLine($"Salary: {emp.Salary}");
            Console.WriteLine($"Manager: {emp.IsManager}");
            Console.WriteLine();
        }
    }

    private static void PrintFilteredDepartments(List<Department> departments)
    {
        foreach (var dept in departments)
        {
            Console.WriteLine($"Short Name: {dept.ShortName}");
            Console.WriteLine($"Long Name: {dept.LongName}");
            Console.WriteLine();
        }
    }
}
