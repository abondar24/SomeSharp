namespace PretendCompany.Commands;

public class CombinedEmployeeCommand : BaseQueryCommand
{
    public override void Execute()
    {
        var combniedEmployees = from emp in _employees
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
        throw new NotImplementedException();
    }

    public override void Execute(string queryParam)
    {
        throw new NotImplementedException();
    }
}