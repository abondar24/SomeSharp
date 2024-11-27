using ClubMember.Views;

namespace ClubMember;


class Program
{

    static void Main(string[] args)
    {
        var mainView = ViewFactory.GetMainViewObject();
        mainView.RunView();

        Console.ReadKey();

    }

}