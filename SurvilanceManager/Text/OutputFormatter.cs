namespace SurvilanceManager.Text;

public static class OutputFormatter
{
    public enum TextTheme
    {
        Security,
        Employee,
        Normal
    }

    public static void ChangeOutputTheme(TextTheme textTheme)
    {
        switch (textTheme)
        {
            case TextTheme.Employee:
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.White;
                break;

            case TextTheme.Security:
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;

            case TextTheme.Normal:
            default:
                Console.ResetColor();
                break;
        }
    }

}