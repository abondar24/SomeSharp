using PretendCompany.Extensions;

namespace PretendCompany.Commands;


public class DepartmentCommand : BaseQueryCommand
{

    public override void Execute(string queryParam)
    {
        var deptData = _departments.Filter(dpt => dpt.ShortName.Equals(queryParam.ToUpper()));

        foreach (var dept in deptData)
        {
            Console.WriteLine($"Short Name: {dept.ShortName}");
            Console.WriteLine($"Long Name: {dept.LongName}");
        }
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}