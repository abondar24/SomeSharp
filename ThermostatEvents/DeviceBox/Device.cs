using ThermostatEvents.Cooling;
using ThermostatEvents.Heat;

namespace ThermostatEvents.DeviceBox;


public class Device : IDevice
{
    private const double Warning_Level = 27;

    private const double Emergency_Level = 75;

    public double WarningLevelTemp => Warning_Level;

    public double EmergencyLevelTemp => Emergency_Level;

    public void HandleEmergency()
    {
        Console.WriteLine();
        Console.WriteLine("Sending out emergency personal");
        ShutDownDevice();
    }

    private void ShutDownDevice()
    {
        Console.WriteLine("Shutting down device");
    }

    public void RunDevice()
    {
        Console.WriteLine();
        Console.WriteLine("Device is running...");

        var coolingMechanism = new CoolingMechanism();
        var temperatureMonitor = new TemperatureMonitor();
        var heatSensor = new HeatSensor(Warning_Level, Emergency_Level, temperatureMonitor);
        var thermostat = new Thermostat(coolingMechanism, heatSensor, this);

        thermostat.RunThermostat();
    }
}