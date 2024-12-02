namespace SurvilanceManager.Observers;

public class ExternalVistorUnsibsciber<ExternalVisitor>(List<IObserver<ExternalVisitor>> observers, IObserver<ExternalVisitor> observer) : IDisposable
{

    List<IObserver<ExternalVisitor>> _observers = observers;

    IObserver<ExternalVisitor> _observer = observer;

    public void Dispose()
    {
        if (_observers.Contains(_observer))
        {
            _observers.Remove(_observer);
        }
    }
}