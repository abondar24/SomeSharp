using PretendCompany.Models;

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

    public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees, decimal salaryThreshold)
    {
        foreach (var emp in employees)
        {
            if (emp.Salary >= salaryThreshold)
            {
                yield return emp;
            }
        }
    }
}