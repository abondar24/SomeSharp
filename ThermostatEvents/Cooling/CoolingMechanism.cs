namespace ThermostatEvents.Cooling;


public class CoolingMechanism : ICoolingMechanism
{
    public void Off()
    {
        Console.WriteLine();
        Console.WriteLine("Cooling is off");
        Console.WriteLine();
    }

    public void On()
    {
        Console.WriteLine();
        Console.WriteLine("Cooling is on");
        Console.WriteLine();
    }
}