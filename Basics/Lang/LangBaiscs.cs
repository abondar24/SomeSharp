namespace Basics.Lang;

public class LangBasics
{

    private static readonly Lazy<LangBasics> _instance = new(() => new LangBasics());

    private LangBasics() { }

    public static LangBasics Instance => _instance.Value;


    public void DrawShape()
    {
        Console.WriteLine("   /|");
        Console.WriteLine("  / |");
        Console.WriteLine(" /  |");
        Console.WriteLine("/___|");
    }


    public void Greet()
    {
        Console.Write("Enter the name: ");
        var name = Console.ReadLine();

        Console.WriteLine("Hello " + name);
    }


    public void Calculator()
    {
        var res = 0.00m;
        var operators = new string[] { "+", "-", "*", "/", "pow" };

        var num1 = 0.00m;
        var num2 = 0.00m;

        try
        {
            Console.Write("Enter the first number: ");
            num1 = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter the second number: ");
            num2 = Convert.ToDecimal(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Not a number entered");
            Environment.Exit(1);
        }


        Console.Write("Enter the operator: ");
        var oper = Console.ReadLine();

        int index = Array.IndexOf(operators, oper);
        switch (index)
        {
            case 0:
                res = num1 + num2;
                break;
            case 1:
                res = num1 - num2;
                break;
            case 2:
                res = num1 * num2;
                break;
            case 3:
                try
                {
                    res = num1 / num2;
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Environment.Exit(1);
                }
                break;

            case 4:
                res = Power(num1, num2);
                break;

            default:
                Console.WriteLine("Unsupported operator. List of supported opertators: " + string.Join(", ", operators));
                break;

        }

        Console.WriteLine(res);

    }

    private decimal Power(decimal baseNum, decimal power)
    {
        decimal res = 1;
        for (int i = 0; i < power; i++)
        {
            res = res * baseNum;
        }


        return res;
    }

    public void Guess()
    {
        var secretWord = "program";
        var guess = "";
        var guessCount = 0;
        var maxGuesses = 5;

        while (guess != secretWord)
        {
            if (guessCount < maxGuesses)
            {
                Console.Write("Enter guess: ");
                guess = Console.ReadLine();
                guessCount++;
            }
            else
            {
                Console.WriteLine("No guesses left");
                break;
            }

        }

        if (guess == secretWord)
        {
            Console.WriteLine("You have won!");
        }

    }



}