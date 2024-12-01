namespace ThermostatEvents.Heat;


public class TemperatureMonitor()
{
    private double[] _temperatureData = [16, 17, 18.5, 22, 94, 79.3, 26.78, 27.8, 26.99, 45, 65, 88.56];

    public event EventHandler<TemperatureEventArgs>? TemperatureMeasured;


    public void StartMonitoring()
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

            TemperatureMeasured?.Invoke(this, evnt);

            System.Threading.Thread.Sleep(1000);
        }
    }
}