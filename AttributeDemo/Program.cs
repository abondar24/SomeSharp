using System.Reflection;

[assembly: AssemblyDescription("Demo description")]

namespace AttributeDemo;

class Program
{
    static void Main(string[] args)
    {
        var asm = typeof(Program).Assembly;
        var asmName = asm.GetName();
        var version = asmName.Version;
        object[] attrs = asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

        var cls = typeof(Program);


        Console.WriteLine($"Class name: {cls.Name}");
        Console.WriteLine($"Assembly name: {asmName}");
        Console.WriteLine($"Assembly version: {version}");

        if (attrs[0] is AssemblyDescriptionAttribute descr)
        {
            Console.WriteLine($"Assebly description: {descr.Description}");
        }


    }
}
