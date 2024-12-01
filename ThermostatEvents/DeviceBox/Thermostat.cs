using ThermostatEvents.Cooling;
using ThermostatEvents.Heat;

namespace ThermostatEvents.DeviceBox;

public class Thermostat(ICoolingMechanism coolingMechanism, IHeatSensor heatSensor, IDevice device) : IThermostat
{

    private ICoolingMechanism _coolingMechanism = coolingMechanism;

    private IHeatSensor _heatSensor = heatSensor;

    private IDevice _device = device;


    public void WireUpHandlers()
    {
        _heatSensor.EmergencyLevelEventHandler += EmergencyLevelEventHandler;
        _heatSensor.WarningLevelEventHandler += WarningLevelEventHandler;
        _heatSensor.NormalLevelEventHandler += NormalLevelEventHandler;

    }

    private void EmergencyLevelEventHandler(object sender, TemperatureEventArgs envt)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine($"Emergency Alert!! (Emergency level is {_device.EmergencyLevelTemp} and above)");

        _device.HandleEmergency();
        Console.ResetColor();
    }


    private void WarningLevelEventHandler(object sender, TemperatureEventArgs envt)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine();
        Console.WriteLine($"Warning Alert!! (Warning level is between {_device.WarningLevelTemp} and {_device.EmergencyLevelTemp})");

        _coolingMechanism.On();
        Console.ResetColor();
    }

    private void NormalLevelEventHandler(object sender, TemperatureEventArgs envt)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        Console.WriteLine($"Info Alert!! Temperature is back to normal (Warning level is between {_device.WarningLevelTemp} and {_device.EmergencyLevelTemp})");

        _coolingMechanism.Off();
        Console.ResetColor();
    }

    public void RunThermostat()
    {
        Console.WriteLine("Thermostat is running");
        WireUpHandlers();
        _heatSensor.RunHeatSensor();
    }
}