using PretendCompany.Extensions;

namespace PretendCompany.Commands;

public class ManagersCommand : BaseQueryCommand
{
    public override void Execute()
    {
        var managers = _employees.Filter(emp => emp.IsManager);

        foreach (var emp in managers)
        {
            Console.WriteLine($"First Name: {emp.FirstName}");
            Console.WriteLine($"Last Name: {emp.LastName}");
            Console.WriteLine($"Salary: {emp.Salary}");
            Console.WriteLine();
        }
    }

    public override void Execute(string queryParam)
    {
        throw new NotImplementedException();
    }
}