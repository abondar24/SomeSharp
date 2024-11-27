namespace ThermostatEvents;

public class TemperatureEventArgs : EventArgs
{
    public double Temperature { get; set; }

    public DateTime CurrentDateTime { get; set; }
}
