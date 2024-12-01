using System;
using ThermostatEvents.DeviceBox;

namespace ThermostatEvents;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Press any key to start device...");
        Console.ReadKey();

        var device = new Device();
        device.RunDevice();

        Console.ReadKey();
    }
}
