using SurvilanceManager.Models;

namespace SurvilanceManager.Observers;

public abstract class BaseExternalVisitorObserver : IObserver<ExternalVisitor>
{
    protected IDisposable _cancellation;

    protected List<ExternalVisitor> _externalVisitors = [];

    public abstract void OnCompleted();

    public abstract void OnError(Exception error);

    public abstract void OnNext(ExternalVisitor value);

    public void Subscribe(IObservable<ExternalVisitor> provider)
    {
        _cancellation = provider.Subscribe(this);

    }

    public void Unsubscribe(IObservable<ExternalVisitor> provider)
    {
        _cancellation.Dispose();
        _externalVisitors.Clear();
    }


    protected void PrintReport(string heading)
    {
        Console.WriteLine();
        Console.WriteLine(heading);
        Console.WriteLine(new string('-', heading.Length));
        Console.WriteLine();

        foreach (var externalVisitor in _externalVisitors)
        {
            externalVisitor.InBuilding = false;

            Console.WriteLine($"{externalVisitor.Id,-6}{externalVisitor.FirstName,-15}{externalVisitor.LastName,-15}{externalVisitor.EntryDateTime.ToString("dd MM yyyy hh:mm:ss tt"),-25}{externalVisitor.ExitDateTime.ToString("dd MM yyyy hh:mm:ss tt"),-25}");
        }

        Console.WriteLine();
        Console.WriteLine();
    }

    protected void HandleVisitor(ExternalVisitor externalVisitor, Action<string> notifyAction)
    {
        var externalVisitorItem = _externalVisitors.FirstOrDefault(ev => ev.Id == externalVisitor.Id);
        if (externalVisitorItem == null)
        {
            _externalVisitors.Add(externalVisitor);
            notifyAction("enter");

        }
        else if (!externalVisitor.InBuilding)
        {
            externalVisitorItem.InBuilding = false;
            externalVisitorItem.ExitDateTime = externalVisitor.ExitDateTime;
            notifyAction("exit");
        }

    }

}
