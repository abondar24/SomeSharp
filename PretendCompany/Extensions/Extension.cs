namespace PretendCompany.Extensions;

public static class Extension
{
    public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
}