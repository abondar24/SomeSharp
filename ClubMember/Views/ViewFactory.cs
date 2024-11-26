namespace ClubMember.Views;

public static class ViewFactory
{
    public static IView GetMainViewObject()
    {
        //TODO: add creation of login and register views

        IView mainView = new MainView(null, null);
        return mainView;
    }
}