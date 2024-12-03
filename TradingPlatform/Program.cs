namespace TradingPlatform;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Method: Main, ThreadId {Environment.CurrentManagedThreadId}");

        var stockmarketData = new StockMarkertData("CSCO", new DateTime(1141, 4, 5), new DateTime(1151, 6, 7));

        var start = DateTime.Now;

        var tasks = new List<Task<decimal[]>>();

        var getOpeningPricesTask = Task.Run(() => stockmarketData.GetOpeningPrices());
        tasks.Add(getOpeningPricesTask);

        var getClosingPricesTask = Task.Run(() => stockmarketData.GetClosingPrices());
        tasks.Add(getClosingPricesTask);

        var getPriceHighsTask = Task.Run(() => stockmarketData.GetPriceHighs());
        tasks.Add(getPriceHighsTask);

        var getPriceLowsTask = Task.Run(() => stockmarketData.GetPriceLows());
        tasks.Add(getPriceLowsTask);

        var calculateStockasticsTask = Task.Run(() => stockmarketData.CalculateStockastics());
        tasks.Add(calculateStockasticsTask);

        var calculateFastMovingAverage = Task.Run(() => stockmarketData.CalculateFastMovingAverage());
        tasks.Add(calculateFastMovingAverage);

        var calculateSlowMovingAverage = Task.Run(() => stockmarketData.CalculateSlowMovingAverage());
        tasks.Add(calculateSlowMovingAverage);

        var calculateUpperBoundBollingerBand = Task.Run(() => stockmarketData.CalculateUpperBoundBollingerBand());
        tasks.Add(calculateSlowMovingAverage);

        var calculateLowerBoundBollingerBand = Task.Run(() => stockmarketData.CalculateLowerBoundBollingerBand());
        tasks.Add(calculateSlowMovingAverage);

        Task.WaitAll([.. tasks]);

        var openingPrices = tasks[0].Result;
        var closingPrices = tasks[1].Result;
        var priceHighs = tasks[2].Result;
        var priceLows = tasks[3].Result;
        var stockastics = tasks[4].Result;
        var fastMovingAvg = tasks[5].Result;
        var slowMovingAvg = tasks[6].Result;
        var upperBoundBollingerBand = tasks[7].Result;
        var lowerBoundBollingerBand = tasks[8].Result;


        var stop = DateTime.Now;
        var timeSpan = stop.Subtract(start);

        Console.WriteLine($"Total time for operations: {timeSpan.Seconds} {(timeSpan.Seconds > 1 ? "seconds" : "second")}");

        DisplayData(openingPrices, closingPrices, priceHighs, priceLows, stockastics, fastMovingAvg,
        slowMovingAvg, upperBoundBollingerBand, lowerBoundBollingerBand);
    }

    private static void DisplayData(decimal[] openingPrices, decimal[] closingPrices, decimal[] priceHighs, decimal[] priceLows,
    decimal[] stockastics, decimal[] fastMovingAvg, decimal[] slowMovingAvg, decimal[] upperBoundBollingerBand, decimal[] lowerBoundBollingerBand)
    {
        Console.WriteLine("Fake chart");

        DisplayArray("Opening prices", openingPrices);
        DisplayArray("Closing prices", closingPrices);
        DisplayArray("Price highs", priceHighs);
        DisplayArray("Price lows", priceLows);
        DisplayArray("Stockastics", stockastics);
        DisplayArray("Fast moving average", fastMovingAvg);
        DisplayArray("Slow moving average", slowMovingAvg);
        DisplayArray("Upper Bound Bollinger Band", upperBoundBollingerBand);
        DisplayArray("Lower Bound Bollinger Band", lowerBoundBollingerBand);
    }

    private static void DisplayArray(string title, decimal[] prices)
    {
        Console.WriteLine(title + ":");
        foreach (var price in prices)
        {
            Console.Write(" ");
            Console.Write(price.ToString("F2"));
            Console.Write(" ");
        }
        Console.WriteLine();
    }

}