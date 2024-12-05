using PretendCompany.Extensions;

namespace PretendCompany.Commands;

public class HighSalaryEmployeeCommand : BaseQueryCommand
{
    public override void Execute()
    {
        throw new NotImplementedException();

    }

    public override void Execute(string queryParam)
    {
        var salaryThreshold = ConvertThreshold(queryParam);
        var highSalariedEmployees = from emp in _employees.GetHighSalariedEmployees(salaryThreshold)
                                    select new
                                    {
                                        FullName = emp.FirstName + " " + emp.LastName,

                                    };
        Console.WriteLine("High salary employees");
        foreach (var emp in highSalariedEmployees)
        {
            Console.WriteLine($"{emp.FullName}");
        }

    }

    private Decimal ConvertThreshold(string queryParam)
    {
        if (decimal.TryParse(queryParam, out decimal res))
        {
            return res;
        }
        else
        {
            return 0.0m;
        }

    }
}