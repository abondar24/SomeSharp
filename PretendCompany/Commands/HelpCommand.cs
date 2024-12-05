namespace PretendCompany.Commands;

public class HelpCommand : ICommand
{

    private readonly Dictionary<string, string> args;

    public HelpCommand()
    {
        args = new(){
       { "mn", "Prints all managers in employee list"},
       { "dept <dept>", "Prints full information about selected department."},
       { "ce", "Prints combined employee data like full name and salary per year"},
       { "hse <threshhold>", "Prints employees with high salary(threshold is provided as query param)"},
       {"ed","Prints all employees with respective department"},
       {"de","Prints all departments with respective employees"},
    };

    }

    public void Execute()
    {
        Console.WriteLine("Supported arguments:");
        foreach (var entry in args)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

    }

    public void Execute(string queryParam)
    {
        throw new NotImplementedException();
    }
}