namespace FieldValidator;

public static class CommonRegexPattern
{
    public const string Email_Address = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    public const string DE_PhoneNumber = @"^\(?(?:(?:0(?:0|11)\)?[\s-]?\(?|\+)49\)?[\s-]?\(?(?:0\)?[\s-]?\(?)?|0)(?:\d{3,5}\)?[\s-]?\d{3,5}[\s-]?\d{4,10})(?:(?:[\s-]?(?:x|ext\.?\s?|\#)\d+)?)$";
    public const string DE_ZipCode = @"\b\d{5}\b";
    public const string Strong_Password = @"(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$";
}