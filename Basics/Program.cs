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
        Greet();
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


    private static void Calculator()
    {

    }

}


