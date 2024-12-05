using PretendCompany.Extensions;

namespace PretendCompany.Commands;


public class DepartmentCommand : BaseQueryCommand
{

    public override void Execute(string queryParam)
    {
        var deptData = _departments.Filter(dpt => dpt.ShortName.Equals(queryParam.ToUpper())).First();

        Console.WriteLine($"Short Name: {deptData.ShortName}");
        Console.WriteLine($"Long Name: {deptData.LongName}");

    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}