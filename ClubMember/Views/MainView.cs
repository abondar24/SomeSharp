using ClubMember.Output;
using ClubMember.FieldValidators;

namespace ClubMember.Views;


class MainView(IView loginView, IView registerView) : IView
{
    public IFieldValidator FieldValidator => null;

    IView _registerView = registerView;
    IView _loginView = loginView;

    public void RunView()
    {
        CommonOutputText.WriteMainHeading();

        Console.WriteLine("Press 'l' to login, press 'r' to register ");

        ConsoleKey key = Console.ReadKey().Key;

        switch (key)
        {
            case ConsoleKey.R:
                _registerView.RunView();
                _loginView.RunView();
                break;

            case ConsoleKey.L:
                _loginView.RunView();
                break;

            default:
                Console.Clear();
                Console.WriteLine("GoodBye");
                Console.ReadKey();
                break;
        }
    }

}