using Basics.Lang;
using Basics.Command;

namespace Basics;

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
        command.Execute();

    }

    private static ICommand GetCommand(string arg)
    {
        return arg switch
        {
            "help" => new HelpCommand(),
            "ds" => new DrawShapeCommand(),
            "gr" => new GreetCommand(),
            "cl" => new CalculatorCommand(),
            "gs" => new GuessCommand(),
            _ => new HelpCommand()
        };
    }

}


