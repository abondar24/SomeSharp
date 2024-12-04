namespace PretendCompany.Extensions;

public static class Extension
{
    public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
    {
        var filtered = new List<T>();

        foreach (T record in records)
        {
            if (func(record))
            {
                filtered.Add(record);
            }
        }

        return filtered;
    }
}