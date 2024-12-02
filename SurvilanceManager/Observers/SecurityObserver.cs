using SurvilanceManager.Models;
using SurvilanceManager.Text;

namespace SurvilanceManager.Observers;

public class SecurityObserver : BaseExternalVisitorObserver
{

    public override void OnCompleted()
    {
        var heading = "Security Daily Visitor's Report";

        PrintReport(heading);
    }

    public override void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public override void OnNext(ExternalVisitor value)
    {
        var externalVisitor = value;

        HandleVisitor(externalVisitor, action =>
             {
                 if (action == "enter")
                 {
                     OutputFormatter.ChangeOutputTheme(OutputFormatter.TextTheme.Security);

                     Console.WriteLine($"Security notfication: Visitor Id({externalVisitor.Id}), Visitor Name: {externalVisitor.FirstName + " "
                         + externalVisitor.LastName} has entered at {externalVisitor.EntryDateTime.ToString("dd MMM yyyy hh:mm:ss tt")}");
                 }
                 else
                 {
                     Console.WriteLine($"Security notification: Visitor ID: {value.Id}, Name: {value.FirstName} {value.LastName} exited at {value.ExitDateTime:dd MMM yyyy hh:mm:ss tt}");
                 }
                 OutputFormatter.ChangeOutputTheme(OutputFormatter.TextTheme.Normal);
                 Console.WriteLine();
             });


    }
}