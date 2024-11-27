namespace ClubMember.Output;

public static class CommonOutputFormat
{
   public static void ChangeFontColor(FontTheme fontTheme)
   {

      switch (fontTheme)
      {
         case FontTheme.Danger:
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            break;

         case FontTheme.Success:
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            break;

         case FontTheme.Default:
         default:
            Console.ResetColor();
            break;
      }



   }
}