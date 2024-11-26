namespace ClubMember.Output;

public static class CommonOutputText
{
    private static string FormatHeading(HeadingType headingType)
    {
        var heading = headingType.ToString();
        return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
    }

    private static void WriteHeading(HeadingType headingType, bool clearConsole = false)
    {
        if (clearConsole)
        {
            Console.Clear();
        }

        Console.WriteLine(FormatHeading(headingType));
        Console.WriteLine();
        Console.WriteLine();

    }

    public static void WriteMainHeading()
    {
        WriteHeading(HeadingType.Club);
    }

    public static void WriteLoginHeading()
    {
        WriteHeading(HeadingType.Login);
    }

    public static void WriteRegisterHeading()
    {
        WriteHeading(HeadingType.Register);
    }


}