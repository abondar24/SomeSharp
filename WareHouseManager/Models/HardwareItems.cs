using WareHouseManager.Properties;

namespace WareHouseManager.Models;

public abstract class HardwareItem : IEntityPrimaryProperties, IEntityAdditionalProperties
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal UnitValue { get; set; }
}
