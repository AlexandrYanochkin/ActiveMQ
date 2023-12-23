using Apache.NMS;

namespace ActiveMQ.PublishSubscribe.Common.Services.Specialized;

public class NonDurableSubscriberClient : SubscriberClient
{
    public NonDurableSubscriberClient(string brokerUri, string clientId, Action<string> logger)
        : base(brokerUri, clientId, logger)
    {
    }

    protected override IMessageConsumer CreateConsumer()
    {
        return Session.CreateConsumer(Topic);
    }
}
