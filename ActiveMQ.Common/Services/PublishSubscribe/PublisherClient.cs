using ActiveMQ.Common.Services.Base;
using Apache.NMS;

namespace ActiveMQ.Common.Services.PublishSubscribe;

public class PublisherClient : BasePublishSubscribeClient
{
    private readonly IMessageProducer _producer;

    public required Func<string> MessageInput { get; set; }

    public PublisherClient(string brokerUri, string clientId, string? topicName = null)
        : base(brokerUri, clientId, topicName)
    {  
        _producer = Session.CreateProducer(Topic);
    }

    public override Task RunAsync()
    {
        return Task.Run(async () =>
        {
            while (true)
            {
                Logger("Write a message:");
                var messageText = MessageInput();
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
