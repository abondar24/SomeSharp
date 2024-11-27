using ClubMember.Data;
using ClubMember.FieldValidators;
using ClubMember.Output;

namespace ClubMember.Views;

public class RegistraionView : IView
{
    IFieldValidator _fieldValidator;

    IRegister _regiser;

    public IFieldValidator FieldValidator { get => _fieldValidator; }

    public RegistraionView(IRegister register, IFieldValidator fieldValidator)
    {
        _fieldValidator = fieldValidator;
        _regiser = register;
    }

    public void RunView()
    {
        CommonOutputText.WriteMainHeading();
        CommonOutputText.WriteRegisterHeading();
        _fieldValidator.FieldArray[(int)UserRegistrationField.Email] = GetInputFromUser(UserRegistrationField.Email, "Please enter email address");
        _fieldValidator.FieldArray[(int)UserRegistrationField.FirstName] = GetInputFromUser(UserRegistrationField.FirstName, "Please enter first name");
        _fieldValidator.FieldArray[(int)UserRegistrationField.LastName] = GetInputFromUser(UserRegistrationField.LastName, "Please enter last name");
        _fieldValidator.FieldArray[(int)UserRegistrationField.Password] = GetInputFromUser(UserRegistrationField.Password,
        $"Please enter your password.{Environment.NewLine} It must contain 1 small-case letter,{Environment.NewLine} 1 capital letter, 1 digit and 1 special character");
        _fieldValidator.FieldArray[(int)UserRegistrationField.PasswordCompare] = GetInputFromUser(UserRegistrationField.PasswordCompare, "Please re-enter the password");
        _fieldValidator.FieldArray[(int)UserRegistrationField.DateOfBirth] = GetInputFromUser(UserRegistrationField.DateOfBirth, "Please enter date of birth");
        _fieldValidator.FieldArray[(int)UserRegistrationField.Street] = GetInputFromUser(UserRegistrationField.Street, "Please enter street");
        _fieldValidator.FieldArray[(int)UserRegistrationField.City] = GetInputFromUser(UserRegistrationField.City, "Please enter city");
        _fieldValidator.FieldArray[(int)UserRegistrationField.ZipCode] = GetInputFromUser(UserRegistrationField.ZipCode, "Please enter zipcode");
        _fieldValidator.FieldArray[(int)UserRegistrationField.PhoneNumber] = GetInputFromUser(UserRegistrationField.PhoneNumber, "Please enter phone number");

        RegisterUser();
    }

    private string GetInputFromUser(UserRegistrationField field, string promptText)
    {
        var fieldVal = "";


        while (!FieldValid(field, fieldVal))
        {
            Console.WriteLine(promptText);
            fieldVal = Console.ReadLine();
        }


        return fieldVal;
    }

    private bool FieldValid(UserRegistrationField field, string fieldVal)
    {
        if (_fieldValidator.ValidatorDelegate((int)field, fieldVal, _fieldValidator.FieldArray, out string invalidMessage))
        {
            CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
            Console.WriteLine(invalidMessage);
            CommonOutputFormat.ChangeFontColor(FontTheme.Default);
            return false;
        }

        return true;
    }

    private void RegisterUser()
    {
        _regiser.Register(_fieldValidator.FieldArray);

        CommonOutputFormat.ChangeFontColor(FontTheme.Success);
        Console.WriteLine("You have successfully registered. Please press any key to login");
    }


}