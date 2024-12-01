
using System.ComponentModel;

namespace ThermostatEvents.Heat;

public class HeatSensor(double warningLevel, double emergencyLevel) : IHeatSensor
{

    double _warningLevel = warningLevel;
    double _emergencyLevel = emergencyLevel;

    bool _hasReachedWarningTemp = false;

    protected EventHandlerList _eventDelegates = new();

    static readonly object _warningLevelKey = new();

    static readonly object _normalLevelKey = new();

    static readonly object _emergencyLevelKey = new();

    private double[] _temperatureData = [16, 17, 18.5, 22, 24, 19.3, 26.78, 27.8, 45, 65, 88.56];

    //TODO: move event this method with array to a separate class 
    private void MonitorTemperature()
    {
        foreach (double temp in _temperatureData)
        {
            var curDate = DateTime.Now;

            Console.ResetColor();
            Console.WriteLine($"DateTime: {curDate}, Temperature: {temp}");

            var evnt = new TemperatureEventArgs
            {
                Temperature = temp,
                CurrentDateTime = curDate
            };

            if (temp >= _emergencyLevel)
            {
                OnEmergencyLevel(evnt);
            }
            else if (temp >= _warningLevel)
            {
                _hasReachedWarningTemp = true;
                OnWarningLevel(evnt);
            }
            else if (temp < _warningLevel && _hasReachedWarningTemp)
            {
                _hasReachedWarningTemp = false;
                OnNormalLevel(evnt);
            }

            System.Threading.Thread.Sleep(1000);
        }
    }


    protected void OnWarningLevel(TemperatureEventArgs evnt)
    {
        ((EventHandler<TemperatureEventArgs>)_eventDelegates[_warningLevelKey])?.Invoke(this, evnt);

    }

    protected void OnEmergencyLevel(TemperatureEventArgs evnt)
    {
        ((EventHandler<TemperatureEventArgs>)_eventDelegates[_emergencyLevel])?.Invoke(this, evnt);
    }

    protected void OnNormalLevel(TemperatureEventArgs evnt)
    {
        ((EventHandler<TemperatureEventArgs>)_eventDelegates[_normalLevelKey])?.Invoke(this, evnt);
    }



    event EventHandler<TemperatureEventArgs> IHeatSensor.EmergencyLevelEventHandler
    {
        add
        {
            _eventDelegates.AddHandler(_emergencyLevelKey, value);
        }

        remove
        {
            _eventDelegates.RemoveHandler(_emergencyLevelKey, value);
        }
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.WarningLevelEventHandler
    {
        add
        {
            _eventDelegates.AddHandler(_warningLevelKey, value);
        }

        remove
        {
            _eventDelegates.RemoveHandler(_warningLevelKey, value);
        }
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.NormalLevelEventHandler
    {
        add
        {
            _eventDelegates.AddHandler(_normalLevelKey, value);
        }

        remove
        {
            _eventDelegates.RemoveHandler(_normalLevelKey, value);
        }
    }

    public void RunHeatSensor()
    {
        Console.WriteLine("Heat sensor is running");
        MonitorTemperature();
    }
}