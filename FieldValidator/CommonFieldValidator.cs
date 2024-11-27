using System.Text.RegularExpressions;

namespace FieldValidator;

public static class CommonFieldValidator
{
    public static Func<string, bool> RequiredFieldValidator { get; } = fieldValue => !string.IsNullOrEmpty(fieldValue);

    public static Func<string, int, int, bool> StringLenValidator { get; } = (fieldValue, minLen, maxLen) => fieldValue.Length >= minLen && fieldValue.Length <= maxLen;
    public static Func<string, (bool, DateTime)> DateValidator { get; } = fieldValue =>
 {
     var dateFormat = "dd/MM/yyyy";  // EU format (day/month/year)
     var dateProvider = new System.Globalization.CultureInfo("en-GB");
     bool isValid = DateTime.TryParseExact(fieldValue, dateFormat, dateProvider, System.Globalization.DateTimeStyles.None, out DateTime validDateTime);
     return (isValid, validDateTime);
 };

    public static Func<string, string, bool> PatternMatchValidator { get; } = (fieldValue, pattern) => new Regex(pattern).IsMatch(fieldValue);

    public static Func<string, string, bool> CompareFieldsValidator { get; } = (fieldValue, fieldValueCompare) => fieldValue.Equals(fieldValueCompare);

}