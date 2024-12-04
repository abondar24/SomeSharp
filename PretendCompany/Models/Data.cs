namespace PretendCompany.Models;

public static class Data
{
    public static List<Employee> GetEmployees()
    {
        return
        [
        new()
        {
            Id = 1,
            FirstName = "Armen",
            LastName = "Muradyan",
            Salary = 100000.0m,
            IsManager = true,
            DepartmentId = 1
        },
        new()
        {
            Id = 2,
            FirstName = "Arsen",
            LastName = "Karapetyan",
            Salary = 50000.0m,
            IsManager = false,
            DepartmentId = 2
        },
        new()
        {
            Id = 3,
            FirstName = "Arman",
            LastName = "Armanyan",
            Salary = 50000.0m,
            IsManager = false,
            DepartmentId = 2
        },
        new()
        {
            Id = 4,
            FirstName = "Gagik",
            LastName = "Pogosyan",
            Salary = 30000.0m,
            IsManager = true,
            DepartmentId = 3
        }
        ];
    }


    public static List<Department> GetDepartments()
    {
        return [
        new ()
        {
            Id = 1,
            ShortName = "HR",
            LongName = "Human Resources"
        },
        new()
        {
            Id = 2,
            ShortName = "FN",
            LongName = "Finance"
        },
        new()
        {
            Id = 3,
            ShortName = "TE",
            LongName = "Technology"
        }
        ];
    }
}