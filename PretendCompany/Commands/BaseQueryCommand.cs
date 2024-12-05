using PretendCompany.Models;

namespace PretendCompany.Commands;

public abstract class BaseQueryCommand : ICommand
{

    protected readonly List<Employee> _employees;
    protected readonly List<Department> _departments;

    public BaseQueryCommand()
    {
        _employees = Data.GetEmployees();
        _departments = Data.GetDepartments();
    }

    public abstract void Execute();

    public abstract void Execute(string queryParam);
}