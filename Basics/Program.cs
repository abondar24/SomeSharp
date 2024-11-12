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

        DrawShape();
        // Greet();
        Calculator();

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


    //TODO: add a list of accepted ops
    //TODO: check for division by zero
    private static void Calculator()
    {
        Console.Write("Enter the first number: ");
        var num1 = Convert.ToDecimal(Console.ReadLine());

        // Console.Write("Enter the operation: ");
        // var operation = Console.ReadLine();

        Console.Write("Enter the second number: ");
        var num2 = Convert.ToDecimal(Console.ReadLine());

        var res = num1 + num2;
        Console.WriteLine(res);

    }

}


