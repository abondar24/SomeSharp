using System.Text.RegularExpressions;

namespace FieldValidator;

public static class CommonFieldValidator
{
    public static Func<string, bool> RequiredFieldValidator { get; } = fieldValue => !string.IsNullOrEmpty(fieldValue);

    public static Func<string, int, int, bool> StringLenValidator { get; } = (fieldValue, minLen, maxLen) => fieldValue.Length >= minLen && fieldValue.Length <= maxLen;
    public static Func<string, (bool, DateTime)> DateValidator { get; } = fieldValue =>
 {
     bool isValid = DateTime.TryParse(fieldValue, out DateTime validDateTime);
     return (isValid, validDateTime);
 };

    public static Func<string, string, bool> PatternMatchValidator { get; } = (fieldValue, pattern) => new Regex(pattern).IsMatch(fieldValue);

    public static Func<string, string, bool> CompareFieldsValidator { get; } = (fieldValue, fieldValueCompare) => fieldValue.Equals(fieldValueCompare);

}