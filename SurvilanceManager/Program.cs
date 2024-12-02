
using SurvilanceManager.Models;
using SurvilanceManager.Observers;

namespace SurvilanceManager;
class Program
{
    //TODO: add class with constants for action types and dateforamt
    static void Main(string[] args)
    {
        Console.Clear();

        var securitySurveilanceHub = new SecuritySurveilanceHub();

        var employeeObserver = new EmployeeObserver(
          new Employee
          {
              Id = 1,
              FirstName = "Bob",
              LastName = "Jones",
              JobTitle = "Janitor"
          }
        );

        var employeeObserver1 = new EmployeeObserver(
         new Employee
         {
             Id = 2,
             FirstName = "Armen",
             LastName = "Murlikyan",
             JobTitle = "Head Janitor"
         }
       );

        var securityObserver = new SecurityObserver();

        employeeObserver.Subscribe(securitySurveilanceHub);
        employeeObserver1.Subscribe(securitySurveilanceHub);
        securityObserver.Subscribe(securitySurveilanceHub);

        securitySurveilanceHub.ConfirmExternalVisitorEntrance(1, "Arman", "Armanyan", "CleanBox", "Head Janitor", DateTime.Parse("12 April 1242 9:00"), 1);
        securitySurveilanceHub.ConfirmExternalVisitorEntrance(2, "Arsen", "Homikyan", "CleanBox", "Manager", DateTime.Parse("12 April 1242 10:00"), 2);

        securitySurveilanceHub.ConfirmExternalVisitorExit(1, DateTime.Parse("12 April 1242 12:00"));
        securitySurveilanceHub.ConfirmExternalVisitorExit(2, DateTime.Parse("12 April 1242 17:00"));

        securitySurveilanceHub.EntryCutoffTimeReached();
        Console.ReadKey();
    }
}
