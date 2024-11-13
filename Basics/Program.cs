using System;

namespace Basics;


//TODO: refactor to accept arguments
//TODO: refactor to have a proper help  
//TODO: refactor to proper classes

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("C# is on");

        // DrawShape();
        // Greet();
        //Calculator();
        Guess();

    }

    private static void DrawShape()
    {
        Console.WriteLine("   /|");
        Console.WriteLine("  / |");
        Console.WriteLine(" /  |");
        Console.WriteLine("/___|");
    }


    private static void Greet()
    {
        Console.Write("Enter the name: ");
        var name = Console.ReadLine();

        Console.WriteLine("Hello " + name);
    }


    private static void Calculator()
    {
        var res = 0.00m;
        var operators = new string[] { "+", "-", "*", "/" };


        Console.Write("Enter the first number: ");
        var num1 = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Enter the second number: ");
        var num2 = Convert.ToDecimal(Console.ReadLine());

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
                if (num2 == 0.00m)
                {
                    Console.WriteLine("Division by zero");
                    return;
                }
                res = num1 / num2;
                break;
            default:
                Console.WriteLine("Unsupported operator. List of supported opertators: " + string.Join(", ", operators));
                break;

        }


        Console.WriteLine(res);

    }

    private static void Guess()
    {
        var secretWord = "program";
        var guess = "";

        while (guess != secretWord)
        {
            Console.Write("Enter guess: ");
            guess = Console.ReadLine();
        }
    }

}


