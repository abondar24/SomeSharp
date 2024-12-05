using System.Runtime.ConstrainedExecution;
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


        var combniedEmployees = from emp in employees
                                select new
                                {
                                    FullName = emp.FirstName + " " + emp.LastName,
                                    AnnualSalary = emp.Salary * 12
                                };

        Console.WriteLine("Combined employee data");
        foreach (var ce in combniedEmployees)
        {
            Console.WriteLine($"{ce.FullName,-20}{ce.AnnualSalary,10}");
        }

        var highSalariedEmployees = from emp in employees.GetHighSalariedEmployees()
                                    select new
                                    {
                                        FullName = emp.FirstName + " " + emp.LastName,

                                    };
        Console.WriteLine("High salary employees");
        foreach (var emp in highSalariedEmployees)
        {
            Console.WriteLine($"{emp.FullName}");
        }


        var employeesInDepartments = from dept in departments
                                     join emp in employees
                                     on dept.Id equals emp.DepartmentId
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

        var departmentsWithEmployees = from dept in departments
                                       join emp in employees
                                       on dept.Id equals emp.DepartmentId
                                       into employeeGroup
                                       select new
                                       {
                                           Employees = employeeGroup,
                                           DepartmentName = dept.LongName
                                       };

        Console.WriteLine("Departments with employees");
        foreach (var dept in departmentsWithEmployees)
        {
            Console.WriteLine($"Department Name: {dept.DepartmentName}");
            foreach (var emp in dept.Employees)
            {
                Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
            }
        }
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
