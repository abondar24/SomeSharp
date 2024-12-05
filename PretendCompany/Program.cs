using PretendCompany.Commands;

namespace PretendCompany;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided. Please use help to see the full list");
            return;
        }

        var command = GetCommand(args[0]);

        if (args.Length > 1)
        {
            command.Execute(args[1]);
        }
        else
        {
            command.Execute();
        }

    }

    private static ICommand GetCommand(string arg)
    {
        return arg switch
        {
            "help" => new HelpCommand(),
            "mn" => new ManagersCommand(),
            "dept" => new DepartmentCommand(),
            "ce" => new CombinedEmployeeCommand(),
            "hse" => new HighSalaryEmployeeCommand(),
            "ed" => new EmployeesDepartmentsCommand(),
            "de" => new DepartmentEmployeeCommand(),
            _ => new HelpCommand()
        };
    }


}
