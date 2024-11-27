namespace ThermostatEvents.Heat;


public interface IHeatSensor
{
    event EventHandler<TemperatureEventArgs> EmergencyLevelEventHandler;

    event EventHandler<TemperatureEventArgs> WarningLevelEventHandler;

    event EventHandler<TemperatureEventArgs> NormalLevelEventHandler;

    void RunHeatSensor();

}