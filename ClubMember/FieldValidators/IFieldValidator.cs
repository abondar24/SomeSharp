namespace ClubMember.FieldValidators;

public delegate bool FieldValidatorDelegate(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage);

public interface IFieldValidator
{
    void InitValidationDelegates();

    string[] FieldArray { get; }

    FieldValidatorDelegate ValidatorDelegate { get; }
}