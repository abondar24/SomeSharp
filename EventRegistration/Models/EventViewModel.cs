
namespace EventRegistration.Models;

public class EventViewModel
{
    public Event Event { get; set; }
    public bool IsRegistered { get; set; }

    public bool IsDrafted { get; set; }
    public List<RegistrationViewModel> Registrations { get; set; }
}