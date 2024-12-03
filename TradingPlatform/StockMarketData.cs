namespace TradingPlatform;

public class StockMarkertData(string stockSymbol, DateTime startDate, DateTime endDate)
{
    string _stockSymbol = stockSymbol;

    DateTime _startDate = startDate;

    DateTime _endDate = endDate;


    public decimal[] GetOpeningPrices()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(GetOpeningPrices)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(1000);

        data = GenerateRandomDecimals();

        return data;

    }

    public decimal[] GetClosingPrices()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(GetClosingPrices)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(1000);

        data = GenerateRandomDecimals();

        return data;

    }


    public decimal[] GetPriceHighs()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(GetPriceHighs)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(1000);

        data = GenerateRandomDecimals();

        return data;

    }

    public decimal[] GetPriceLows()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(GetPriceLows)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(1000);

        data = GenerateRandomDecimals();

        return data;

    }

    public decimal[] CalculateStockastics()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(CalculateStockastics)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(10000);

        data = GenerateRandomDecimals();

        return data;

    }

    public decimal[] CalculateFastMovingAverage()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(CalculateFastMovingAverage)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(6000);

        data = GenerateRandomDecimals();

        return data;

    }

    public decimal[] CalculateSlowMovingAverage()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(CalculateSlowMovingAverage)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(7000);

        data = GenerateRandomDecimals();

        return data;

    }

    public decimal[] CalculateUpperBoundBollingerBand()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(CalculateUpperBoundBollingerBand)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(5000);

        data = GenerateRandomDecimals();

        return data;

    }

    public decimal[] CalculateLowerBoundBollingerBand()
    {
        decimal[] data;

        Console.WriteLine($"Method name: {nameof(CalculateLowerBoundBollingerBand)}, ThreadId: {Environment.CurrentManagedThreadId}");

        Thread.Sleep(5000);

        data = GenerateRandomDecimals();

        return data;

    }


    private static decimal[] GenerateRandomDecimals()
    {
        var random = new Random();
        decimal[] decimals = new decimal[20];

        for (int i = 0; i < 20; i++)
        {
            decimals[i] = (decimal)(random.NextDouble() * 100); // Random decimals between 0 and 100
        }

        return decimals;
    }

}