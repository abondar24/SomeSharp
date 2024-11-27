namespace ThermostatEvents.DeviceBox;


public interface IDevice
{

    double WarningLevelTemp { get; }

    double EmergencyLevelTemp { get; }

    void RunDevice();

    void HandleEmergency();
}