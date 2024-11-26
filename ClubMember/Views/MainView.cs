using ClubMember.Output;
using ClubMember.FieldValidator;

namespace ClubMember.Views;


class MainView : IView
{
    public IFieldValidator FieldValidator => null;

    IView _registerView;
    IView _loginView;

    public MainView(IView loginView, IView registerView)
    {
        _loginView = loginView;
        _registerView = registerView;
    }

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