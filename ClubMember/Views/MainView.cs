using System;

namespace ClubMember.Views;


class MainView : IView
{
    public IFieldValidator FieldValidator => null;

    public void RunView()
    {
        Console.WriteLine("Press 'l' to login, press 'r' to register ");

        ConsoleKey key = Console.ReadKey().Key;
    }

}