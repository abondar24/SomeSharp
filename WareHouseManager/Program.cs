using WareHouseManager.Models;
using WareHouseManager.Queue;

namespace WareHouseManager;


class Program
{
    static void Main(string[] args)
    {
        CustomQueue<HardwareItem> hardwareQueue = new();

        hardwareQueue.CustomQueueEventHandler += CustomQueueEventHandler;

        System.Threading.Thread.Sleep(2000);

        SeedData(hardwareQueue);
    }


    private static void SeedData(CustomQueue<HardwareItem> hardwareQueue)
    {
        hardwareQueue.AddItem(new Drill { Id = 1, Name = "Drill 1", Type = "Drill", UnitValue = 20.00m, Quantity = 10 });
        System.Threading.Thread.Sleep(1000);

        hardwareQueue.AddItem(new Drill { Id = 2, Name = "Drill 2", Type = "Drill", UnitValue = 30.00m, Quantity = 20 });
        System.Threading.Thread.Sleep(2000);

        hardwareQueue.AddItem(new Ladder { Id = 3, Name = "Ladder 1", Type = "Ladder", UnitValue = 100.00m, Quantity = 5 });
        System.Threading.Thread.Sleep(1000);

        hardwareQueue.AddItem(new Hammer { Id = 4, Name = "Hammer 1", Type = "Hammer", UnitValue = 10.00m, Quantity = 80 });
        System.Threading.Thread.Sleep(3000);

        hardwareQueue.AddItem(new PaintBrush { Id = 5, Name = "Paint Brush 1", Type = "PaintBrush", UnitValue = 5.00m, Quantity = 100 });
        System.Threading.Thread.Sleep(3000);

        hardwareQueue.AddItem(new PaintBrush { Id = 6, Name = "Paint Brush 2", Type = "PaintBrush", UnitValue = 5.00m, Quantity = 100 });
        System.Threading.Thread.Sleep(3000);

        hardwareQueue.AddItem(new PaintBrush { Id = 7, Name = "Paint Brush 3", Type = "PaintBrush", UnitValue = 5.00m, Quantity = 100 });
        System.Threading.Thread.Sleep(3000);

        hardwareQueue.AddItem(new Hammer { Id = 8, Name = "Hammer 2", Type = "Hammer", UnitValue = 11.00m, Quantity = 80 });
        System.Threading.Thread.Sleep(3000);

        hardwareQueue.AddItem(new Hammer { Id = 9, Name = "Hammer 3", Type = "Hammer", UnitValue = 13.00m, Quantity = 80 });
        System.Threading.Thread.Sleep(3000);

        hardwareQueue.AddItem(new Hammer { Id = 10, Name = "Hammer 4", Type = "Hammer", UnitValue = 14.00m, Quantity = 80 });
        System.Threading.Thread.Sleep(3000);
    }

    private static void CustomQueueEventHandler(CustomQueue<HardwareItem> sender, QueueEvent queueEvent)
    {
        Console.Clear();

        if (sender.Length > 0)
        {
            Console.WriteLine(queueEvent.Message);
            Console.WriteLine();
            Console.WriteLine();

            PrintQueueValues(sender);
            if (sender.Length == Constants.Batch_Size)
            {
                Console.WriteLine("Processing Queue");
                Console.WriteLine();

                ProcessItems(sender);
            }
        }
        else
        {
            Console.WriteLine("Status: All items have been processed");
        }
    }

    public static void ProcessItems(CustomQueue<HardwareItem> customQueue)
    {
        while (customQueue.Length > 0)
        {
            System.Threading.Thread.Sleep(3000);
            var item = customQueue.GetItem();
        }
    }


    private static void PrintQueueValues(CustomQueue<HardwareItem> hardwareItems)
    {
        foreach (var hardwareItem in hardwareItems)
        {
            Console.WriteLine($"{hardwareItem.Id,-6}{hardwareItem.Name,-15}{hardwareItem.Type,-20}{hardwareItem.Quantity,10}{hardwareItem.UnitValue,10}");
        }

    }
}
