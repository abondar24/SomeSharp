namespace Basics.Command;

public class HelpCommand : ICommand
{

    private readonly Dictionary<string, string> args;

    public HelpCommand()
    {
        args = new(){
       { "ds", "Draws a shape"},
       { "gr", "Greets a user"},
       { "cl", "A dummy calculator with basic math operations"},
       { "gs","Guess game - guess a word program within 5 attempts"}
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


}