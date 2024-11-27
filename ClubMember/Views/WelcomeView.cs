using ClubMember.FieldValidators;
using ClubMember.Models;
using ClubMember.Output;

namespace ClubMember.Views;


public class WelcomeView(User user) : IView
{
    public IFieldValidator FieldValidator => null;

    User _user = user;

    public void RunView()
    {
        Console.Clear();
        CommonOutputText.WriteMainHeading();

        CommonOutputFormat.ChangeFontColor(FontTheme.Success);
        Console.WriteLine($"Hello {_user.FirstName}!!{Environment.NewLine} Welcome to the club");
        CommonOutputFormat.ChangeFontColor(FontTheme.Default);
        Console.ReadKey();
    }
}