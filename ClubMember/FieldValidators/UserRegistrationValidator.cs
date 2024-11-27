using ClubMember.Data;
using FieldValidator;

namespace ClubMember.FieldValidators;

public class UserRegistrationValidator(IRegister register) : IFieldValidator
{
    const int FirstName_Min_Length = 2;
    const int FirstName_Max_Length = 100;

    const int LastName_Min_Length = 2;
    const int LasttName_Max_Length = 100;

    FieldValidatorDelegate? _fieldValidatorDelegate;

    Func<string, bool>? _emailExistsFunc;

    Func<string, bool>? _requiredValidFunc;
    Func<string, int, int, bool>? _stringLengthValidFunc;
    Func<string, string, bool>? _patternMatchValidFunc;
    Func<string, string, bool>? _compareFieldsValidFunc;
    Func<string, (bool, DateTime)>? _dateValidFunc;


    string[]? _fieldArray;

    public string[] FieldArray
    {
        get
        {
            _fieldArray ??= new string[Enum.GetValues(typeof(UserRegistrationField)).Length];
            return _fieldArray;
        }
    }

    public FieldValidatorDelegate ValidatorDelegate => ValidField;

    private readonly IRegister _register = register;

    public void InitValidationDelegates()
    {
        _fieldValidatorDelegate = new FieldValidatorDelegate(ValidField);

        _emailExistsFunc = new Func<string, bool>(_register.EmailExists);

        _requiredValidFunc = CommonFieldValidator.RequiredFieldValidator;
        _stringLengthValidFunc = CommonFieldValidator.StringLenValidator;
        _dateValidFunc = CommonFieldValidator.DateValidator;
        _patternMatchValidFunc = CommonFieldValidator.PatternMatchValidator;
        _compareFieldsValidFunc = CommonFieldValidator.CompareFieldsValidator;
    }

    private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
    {
        fieldInvalidMessage = string.Empty;
        var userRegistrationField = (UserRegistrationField)fieldIndex;


        string? CheckRequiredField(string value, UserRegistrationField field)
        {
            return _requiredValidFunc != null && !_requiredValidFunc(value)
                ? $"You must enter a value for field: {Enum.GetName(typeof(UserRegistrationField), field)}{Environment.NewLine}"
                : null;
        }

        string? CheckLength(string value, int minLength, int maxLength, UserRegistrationField field)
        {
            return _stringLengthValidFunc != null && !_stringLengthValidFunc(value, minLength, maxLength)
                ? $"The length for field: {Enum.GetName(typeof(UserRegistrationField), field)} must be between {minLength} and {maxLength}{Environment.NewLine}"
                : null;
        }


        string? CheckPattern(string value, string pattern, string errorMessage)
        {
            return _patternMatchValidFunc != null && !_patternMatchValidFunc(value, pattern)
                ? errorMessage
                : null;
        }


        string? CheckFieldsMatch(string value, string compareValue, string errorMessage)
        {
            return _compareFieldsValidFunc != null && !_compareFieldsValidFunc(value, compareValue)
                ? errorMessage
                : null;
        }

        string? CheckDateValid(string value)
        {
            if (_dateValidFunc != null)
            {
                var (isValidDate, validDateTime) = _dateValidFunc(value);
                return !isValidDate ? "You did not enter a valid date." : null;

            }

            return "Date validation function is not configured.";

        }

        string? CheckEmailExists(string value)
        {
            return _emailExistsFunc != null && _emailExistsFunc(value) ? $"Email already exists {Environment.NewLine}" : null;
        }

        fieldInvalidMessage = userRegistrationField switch
        {
            UserRegistrationField.Email => CheckRequiredField(fieldValue, userRegistrationField)
                          ?? CheckPattern(fieldValue, CommonRegexPattern.Email_Address, "You must enter a valid email address: " + Environment.NewLine)
                            ?? CheckEmailExists(fieldValue),
            UserRegistrationField.FirstName => CheckRequiredField(fieldValue, userRegistrationField)
                                            ?? CheckLength(fieldValue, FirstName_Min_Length, FirstName_Max_Length, userRegistrationField),
            UserRegistrationField.LastName => CheckRequiredField(fieldValue, userRegistrationField)
                                                  ?? CheckLength(fieldValue, LastName_Min_Length, LasttName_Max_Length, userRegistrationField),
            UserRegistrationField.Password => CheckRequiredField(fieldValue, userRegistrationField)
                                                  ?? CheckPattern(fieldValue, CommonRegexPattern.Strong_Password, "Your password must contain at least 1 small-case letter, 1 capital letter, 1 special character and the length should be between 6 - 10 characters" + Environment.NewLine),
            UserRegistrationField.PasswordCompare => CheckRequiredField(fieldValue, userRegistrationField)
                                                  ?? CheckFieldsMatch(fieldValue, fieldArray[(int)UserRegistrationField.Password], "Your entry did not match your password" + Environment.NewLine),
            UserRegistrationField.DateOfBirth => CheckRequiredField(fieldValue, userRegistrationField) ?? CheckDateValid(fieldValue),
            UserRegistrationField.PhoneNumber => CheckRequiredField(fieldValue, userRegistrationField)
                                                  ?? CheckPattern(fieldValue, CommonRegexPattern.DE_PhoneNumber, "You did not enter a valid DE phone number" + Environment.NewLine),
            UserRegistrationField.Street or UserRegistrationField.City => CheckRequiredField(fieldValue, userRegistrationField),
            UserRegistrationField.ZipCode => (string)(CheckRequiredField(fieldValue, userRegistrationField)
                                                  ?? CheckPattern(fieldValue, CommonRegexPattern.DE_ZipCode, "You did not enter a valid DE post code" + Environment.NewLine)),
            _ => throw new ArgumentException("This field doesn't exist"),
        };
        return string.IsNullOrEmpty(fieldInvalidMessage);
    }


}