using Apache.NMS;

namespace ActiveMQ.Common.Services.PublishSubscribe.Subscribers;

public class VirtualSubscriberClient : SubscriberClient
{
    private readonly IQueue _queue;

    public VirtualSubscriberClient(string brokerUri, string clientId, string queueName)
        : base(brokerUri, clientId, null)
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