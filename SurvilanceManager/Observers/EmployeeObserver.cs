using SurvilanceManager.Models;
using SurvilanceManager.Text;

namespace SurvilanceManager.Observers;

public class EmployeeObserver(IEmployee employee) : BaseExternalVisitorObserver
{

    IEmployee _employee = employee;


    public override void OnCompleted()
    {
        var heading = $"{_employee.FirstName + " " + _employee.LastName} Daily Visitor's Report";

        PrintReport(heading);
    }

    public override void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public override void OnNext(ExternalVisitor value)
    {
        var externalVisitor = value;

        if (externalVisitor.EmployeeContactId == _employee.Id)
        {
            HandleVisitor(externalVisitor, action =>
            {
                if (action == "enter")
                {
                    OutputFormatter.ChangeOutputTheme(OutputFormatter.TextTheme.Employee);

                    Console.WriteLine($"{_employee.FirstName + " "
                    + _employee.LastName}, your visitory has arrived. Visitor Id({externalVisitor.Id}), Visitor Name: {externalVisitor.FirstName + " "
                    + externalVisitor.LastName} has entered at {externalVisitor.EntryDateTime.ToString("dd MMM yyyy hh:mm:ss tt")}");
                }
                else
                {
                    Console.WriteLine($"Visitor ID: {value.Id} has exited at {value.ExitDateTime:dd MMM yyyy hh:mm:ss tt}");
                }

                OutputFormatter.ChangeOutputTheme(OutputFormatter.TextTheme.Normal);
                Console.WriteLine();

            });
        }

    }
}