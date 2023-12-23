using Apache.NMS;

namespace ActiveMQ.PublishSubscribe.Common.Services.Specialized;

public class DurableSubscriberClient : SubscriberClient
{
    public DurableSubscriberClient(string brokerUri, string clientId, Action<string> logger)
        : base(brokerUri, clientId, logger)
    {
    }

    protected override IMessageConsumer CreateConsumer()
    {
        return Session.CreateDurableConsumer(Topic, ConsumerName);
    }
}
