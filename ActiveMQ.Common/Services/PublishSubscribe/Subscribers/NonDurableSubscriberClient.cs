using Apache.NMS;

namespace ActiveMQ.Common.Services.PublishSubscribe.Subscribers;

public class NonDurableSubscriberClient : SubscriberClient
{
    public NonDurableSubscriberClient(string brokerUri, string clientId, string? topicName = null)
        : base(brokerUri, clientId, topicName)
    {
    }

    protected override IMessageConsumer CreateConsumer()
    {
        return Session.CreateConsumer(Topic);
    }
}
