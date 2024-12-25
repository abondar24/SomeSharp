
namespace EventRegistration.Models;

public class EventViewModel : BaseEventViewModel
{
    public bool IsDrafted { get; set; }
    public List<RegistrationViewModel> Registrations { get; set; }
}