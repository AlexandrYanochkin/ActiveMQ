using Apache.NMS;

namespace ActiveMQ.PublishSubscribe.Common.Services.Specialized;

public class PublisherClient : BasePublishSubscribeClient
{
    private readonly IMessageProducer _producer;
    private readonly Func<string> _messageInput;

    public PublisherClient(string brokerUri, string clientId, Action<string> logger, Func<string> messageInput, string topicName = null)
        : base(brokerUri, clientId, topicName, logger)
    {  
        _producer = Session.CreateProducer(Topic);
        _messageInput = messageInput ?? throw new ArgumentNullException(nameof(messageInput));
    }

    public override Task RunAsync()
    {
        return Task.Run(async () =>
        {
            while (true)
            {
                Logger("Write a message:");
                var messageText = _messageInput();
                var message = await _producer.CreateTextMessageAsync(messageText);

                await _producer.SendAsync(message);
            }
        });
    }

    protected override void Dispose(bool disposing) 
    { 
        base.Dispose(disposing);

        if (disposing) 
        {
            _producer.Dispose();
        }
    }
}
