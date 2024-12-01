
using System.ComponentModel;

namespace ThermostatEvents.Heat;

public class HeatSensor : IHeatSensor
{

    double _warningLevel;
    double _emergencyLevel;

    TemperatureMonitor _temperatureMonitor;

    protected EventHandlerList _eventDelegates = new();

    static readonly object _warningLevelKey = new();

    static readonly object _normalLevelKey = new();

    static readonly object _emergencyLevelKey = new();

    public HeatSensor(double warningLevel, double emergencyLevel, TemperatureMonitor temperatureMonitor)
    {
        _warningLevel = warningLevel;
        _emergencyLevel = emergencyLevel;
        _temperatureMonitor = temperatureMonitor;
        _temperatureMonitor.TemperatureMeasured += OnTemperatureMeasured;
    }

    private void OnTemperatureMeasured(object? sender, TemperatureEventArgs evnt)
    {
        var temp = evnt.Temperature;

        if (temp >= _emergencyLevel)
        {
            OnEmergencyLevel(evnt);
        }
        else if (temp >= _warningLevel)
        {
            OnWarningLevel(evnt);
        }
        else if (temp < _warningLevel)
        {
            OnNormalLevel(evnt);
        }
    }


    protected void OnWarningLevel(TemperatureEventArgs evnt) => ((EventHandler<TemperatureEventArgs>)_eventDelegates[_warningLevelKey])?.Invoke(this, evnt);


    protected void OnEmergencyLevel(TemperatureEventArgs evnt) => ((EventHandler<TemperatureEventArgs>)_eventDelegates[_emergencyLevelKey])?.Invoke(this, evnt);

    protected void OnNormalLevel(TemperatureEventArgs evnt) => ((EventHandler<TemperatureEventArgs>)_eventDelegates[_normalLevelKey])?.Invoke(this, evnt);


    event EventHandler<TemperatureEventArgs> IHeatSensor.EmergencyLevelEventHandler
    {
        add => _eventDelegates.AddHandler(_emergencyLevelKey, value);
        remove => _eventDelegates.RemoveHandler(_emergencyLevelKey, value);

    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.WarningLevelEventHandler
    {
        add => _eventDelegates.AddHandler(_warningLevelKey, value);
        remove => _eventDelegates.RemoveHandler(_warningLevelKey, value);
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.NormalLevelEventHandler
    {
        add => _eventDelegates.AddHandler(_normalLevelKey, value);
        remove => _eventDelegates.RemoveHandler(_normalLevelKey, value);
    }

    public void RunHeatSensor()
    {
        Console.WriteLine("Heat sensor is running");
        _temperatureMonitor.StartMonitoring();
    }
}