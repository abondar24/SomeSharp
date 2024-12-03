using System.Text;

namespace TodoApiClient;

class Program
{
    private const string API_URL = "https://jsonplaceholder.typicode.com/todos/";


    static async Task Main(string[] args)
    {
        Console.WriteLine("Please, enter required todo ID");
        var todoId = Console.ReadLine();

        if (VerifyId(todoId))
        {
            var data = await DownloadData(todoId);

            Console.WriteLine();
            Console.WriteLine("Got todo:");
            Console.WriteLine(data);
        }
        else
        {
            Console.WriteLine("Invalid ID. Please enter a valid integer.");
        }

    }

    private static bool VerifyId(string todoId)
    {
        return int.TryParse(todoId, out int number) && number > 0;
    }

    private async static Task<string> DownloadData(string todoId)
    {
        using var client = new HttpClient();

        var response = await client.GetAsync(API_URL + todoId);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            return $"Error: {response.StatusCode}";
        }
    }
}
