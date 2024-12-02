using SurvilanceManager.Models;

namespace SurvilanceManager.Observers;

public class SecuritySurveilanceHub : IObservable<ExternalVisitor>
{
    private List<ExternalVisitor> _externalVistors;

    private List<IObserver<ExternalVisitor>> _observers;


    public SecuritySurveilanceHub()
    {
        _externalVistors = [];
        _observers = [];
    }

    public void ConfirmExternalVisitorEntrance(int id, string firstName, string lastName, string companyName, string jobTitle, DateTime entryDateTime, int employeeContactId)
    {
        var externalVisitor = new ExternalVisitor
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            CompanyName = companyName,
            JobTitle = jobTitle,
            EntryDateTime = entryDateTime,
            EmployeeContactId = employeeContactId,
            InBuilding = true
        };

        _externalVistors.Add(externalVisitor);

        foreach (var observer in _observers)
        {
            observer.OnNext(externalVisitor);
        }

    }

    public void ConfirmExternalVisitorExit(int externalVisitorId, DateTime exitDateTime)
    {
        var externalVisitor = _externalVistors.FirstOrDefault(ev => ev.Id == externalVisitorId);

        if (externalVisitor != null)
        {
            externalVisitor.ExitDateTime = exitDateTime;
            externalVisitor.InBuilding = false;

            foreach (var observer in _observers)
            {
                observer.OnNext(externalVisitor);
            }
        }
    }

    public void EntryCutoffTimeReached()
    {
        var visitorsInBuildingCount = _externalVistors.Where(ev => ev.InBuilding == true).ToList().Count;
        if (visitorsInBuildingCount == 0)
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }
    }

    public IDisposable Subscribe(IObserver<ExternalVisitor> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }


        foreach (var externalVisitor in _externalVistors)
        {
            observer.OnNext(externalVisitor);
        }

        return new ExternalVistorUnsibsciber<ExternalVisitor>(_observers, observer);
    }
}

