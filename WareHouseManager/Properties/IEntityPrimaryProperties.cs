namespace WareHouseManager.Properties;


public interface IEntityPrimaryProperties
{
    int Id { get; set; }

    string Name { get; set; }

    string Type { get; set; }
}