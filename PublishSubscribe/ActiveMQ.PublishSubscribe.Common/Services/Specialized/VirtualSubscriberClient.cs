using Apache.NMS;

namespace ActiveMQ.PublishSubscribe.Common.Services.Specialized;

public class VirtualSubscriberClient : SubscriberClient
{
    private readonly IQueue _queue;

    public VirtualSubscriberClient(string brokerUri, string clientId, Action<string> logger, string queueName)
        : base(brokerUri, clientId, logger)
    {
        _queue = Session.GetQueue(queueName);
    }

    protected override IMessageConsumer CreateConsumer()
    {
        return Session.CreateConsumer(_queue);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            _queue.Dispose();
        }
    }
}


//private readonly int _id;
//private readonly IQueue _queue;
//private readonly IMessageConsumer _consumer;

//public VirtualSubscriberClient(string brokerUri, string clientId, int subscriberId, Action<string> logger, string queueName)
//    : base(brokerUri, clientId, null, logger)
//{
//    _id = subscriberId;
//    _queue = Session.GetQueue(queueName);
//    _consumer = Session.CreateConsumer(_queue);
//}

//public override Task RunAsync()
//{
//    _consumer.Listener += message =>
//    {
//        var messageBody = message.Body<string>();

//        Console.WriteLine($"Consumer[{_id}] received a message:\t{messageBody}");
//    };

//    return Task.CompletedTask;
//}

//protected override void Dispose(bool disposing)
//{
//    base.Dispose(disposing);

//    if (disposing)
//    {
//        _queue.Dispose();
//        _consumer.Dispose();
//    }
//}