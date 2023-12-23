using Apache.NMS;

namespace ActiveMQ.Common.Services.PublishSubscribe.Subscribers;

public class DurableSubscriberClient : SubscriberClient
{
    public DurableSubscriberClient(string brokerUri, string clientId, string? topicName = null)
        : base(brokerUri, clientId, topicName)
    {
    }

    protected override IMessageConsumer CreateConsumer()
    {
        return Session.CreateDurableConsumer(Topic, ConsumerName);
    }
}
