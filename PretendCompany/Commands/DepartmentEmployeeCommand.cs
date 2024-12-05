namespace PretendCompany.Commands;

public class DepartmentEmployeeCommand : BaseQueryCommand
{
    public override void Execute()
    {
        var departmentsWithEmployees = from dept in _departments
                                       join emp in _employees
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

    public override void Execute(string queryParam)
    {
        throw new NotImplementedException();
    }
}