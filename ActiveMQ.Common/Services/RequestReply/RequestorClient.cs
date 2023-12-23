using ActiveMQ.Common.Services.Base;

namespace ActiveMQ.Common.Services.RequestReply;

public class RequestorClient : BaseRequestReplyClient
{
    public const int MinTextMessageLength = 5;

    private int _textMessageLength;

    public required int TextMessageLength 
    {
        get => _textMessageLength;

        set => _textMessageLength = value >= MinTextMessageLength ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public required Func<int, string> CreateRandomMessage { get; set; }

    public RequestorClient(string brokerUri)
        : base(brokerUri)
    {
    }

    public override Task RunAsync()
    {
        return Task.Run(async () =>
        {
            var producer = await Session.CreateProducerAsync(RequestorQueue);
            var consumer = await Session.CreateConsumerAsync(ReplierQueue);

            Logger($"The requestor has been started");

            while (true)
            {
                var randomText = CreateRandomMessage(_textMessageLength);
                var message = await producer.CreateTextMessageAsync(randomText);
                Logger($"Requestor sending a message:\t{randomText}");

                await producer.SendAsync(message);
                await Task.Delay(DelayTime);

                var replyMessage = await consumer.ReceiveAsync();
                var replyMessageBody = replyMessage.Body<string>();
                Logger($"Requestor receiving a reply:\t{replyMessageBody}");
            }
        });
    }
}
