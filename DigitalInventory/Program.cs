using DigitalInventory.Product;

namespace DigitalInventory;

class Program
{
    //BASED ON https://github.com/GavinLonDigital/DigitalProductInventoryApplication/blob/master/DigitalProductInventoryApplication/Program.cs
    static void Main(string[] args)
    {
        var products = new List<ProductBase>();


        var digitalBook = Factory<DigitalBook, ProductBase>.GetInstance();
        AddPropertiesToProduct(digitalBook, 1, "The Old Man and the Sea", 1);
        products.Add(digitalBook);

        var movie = Factory<Movie, ProductBase>.GetInstance();
        AddPropertiesToProduct(movie, 2, "Highlander", 2);
        products.Add(movie);

        movie = Factory<Movie, ProductBase>.GetInstance();
        AddPropertiesToProduct(movie, 3, "Shawshank Redemption", 2);
        products.Add(movie);

        var album = Factory<MusicRecording, ProductBase>.GetInstance();
        AddPropertiesToProduct(album, 4, "Iron Man Soundtrack", 3);
        products.Add(album);


        foreach (var result in products)
        {
            Console.WriteLine($"Product Id: {result.Id}");
            Console.WriteLine($"Title: {result.Title}");
            Console.WriteLine();

        }
    }

    private static void AddPropertiesToProduct(ProductBase product, int id, string title, int categoryId)
    {
        product.Id = id;
        product.Title = title;
        product.CategoryId = categoryId;

    }
}
