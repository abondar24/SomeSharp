namespace DigitalInventory.Product;

public abstract class ProductBase : IPrimaryProperties
{
    public int Id { get; set; }
    public string Title { get; set; }

    public int CategoryId { get; set; }
}