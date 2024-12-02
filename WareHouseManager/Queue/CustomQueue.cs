using WareHouseManager.Properties;

namespace WareHouseManager.Queue;


public delegate void QueueEventHandler<T, U>(T sender, U evnt);

public class CustomQueue<T> where T : IEntityPrimaryProperties, IEntityAdditionalProperties
{
    Queue<T> _queue;

    public event QueueEventHandler<CustomQueue<T>, QueueEvent> CustomQueueEventHandler;

    public CustomQueue()
    {
        _queue = new Queue<T>();
    }

    public int Length
    {
        get => _queue.Count;
    }

    public void AddItem(T item)
    {
        _queue.Enqueue(item);

        var queueEvent = new QueueEvent
        {
            Message = $"{DateTime.Now.ToString(Constants.Date_Format)},Id ({item.Id}),Name ({item.Name}), Type ({item.Type}), Quantity({item.Quantity}), has been added to the queue."
        };

        OnQueueChanged(queueEvent);
    }

    public T GetItem()
    {
        T item = _queue.Dequeue();
        var queueEvent = new QueueEvent
        {
            Message = $"{DateTime.Now.ToString(Constants.Date_Format)},Id ({item.Id}),Name ({item.Name}), Type ({item.Type}), Quantity({item.Quantity}), has been processed."
        };
        OnQueueChanged(queueEvent);

        return item;
    }

    protected virtual void OnQueueChanged(QueueEvent evnt)
    {
        CustomQueueEventHandler(this, evnt);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _queue.GetEnumerator();
    }

}