using ClubMember.Data;
using ClubMember.FieldValidators;
using ClubMember.Output;

namespace ClubMember.Views;


public class LoginView(ILogin login) : IView
{
    public IFieldValidator FieldValidator => null;

    ILogin _login = login;

    public void RunView()
    {
        CommonOutputText.WriteMainHeading();
        CommonOutputText.WriteLoginHeading();

        Console.WriteLine("Please enter an email address");
        var email = Console.ReadLine();

        Console.WriteLine("Please enter the password");
        var password = Console.ReadLine();

        var user = _login.Login(email, password);
        if (user != null)
        {
            var welcomeView = new WelcomeView(user);
            welcomeView.RunView();
        }
        else
        {
            Console.Clear();
            CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
            Console.WriteLine("Invalid credentiasls");
            CommonOutputFormat.ChangeFontColor(FontTheme.Default);
            Console.ReadKey();
        }
    }


}