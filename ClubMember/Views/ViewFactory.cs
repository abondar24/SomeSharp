namespace ClubMember.Views;

using ClubMember.Data;
using ClubMember.FieldValidators;

public static class ViewFactory
{
    public static IView GetMainViewObject()
    {

        var login = new LoginUser();
        var register = new RegisterUser();

        var validator = new UserRegistrationValidator(register);
        validator.InitValidationDelegates();

        var registerView = new RegistraionView(register, validator);
        var loginView = new LoginView(login);


        return new MainView(loginView, registerView);
    }
}