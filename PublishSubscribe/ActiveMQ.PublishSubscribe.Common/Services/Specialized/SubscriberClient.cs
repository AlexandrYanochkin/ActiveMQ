using Apache.NMS;

namespace ActiveMQ.PublishSubscribe.Common.Services.Specialized;

public abstract class SubscriberClient : BasePublishSubscribeClient
{
    protected string ConsumerName { get; }

    private IMessageConsumer _consumer;

    protected IMessageConsumer Consumer
    {
        get
        {
            _consumer ??= CreateConsumer();

            return _consumer;
        }
    }

    public SubscriberClient(string brokerUri, string clientId, Action<string> logger)
        : base(brokerUri, clientId, null, logger)
    {
        ConsumerName = clientId;
    }

    public override Task RunAsync()
    {   
        Consumer.Listener += message =>
        {
            var messageBody = message.Body<string>();

            Logger($"{ConsumerName}:\t{messageBody}");
        };

        return Task.CompletedTask;
    }

    protected abstract IMessageConsumer CreateConsumer();

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            _consumer.Dispose();
        }
    }
}