using ActiveMQ.Common.Services.Base;

namespace ActiveMQ.Common.Services.RequestReply;

public class ReplierClient : BaseRequestReplyClient
{
    public required Func<string, string> TextProcessor { get; init; }

    public ReplierClient(string brokerUri)
        : base(brokerUri)
    {
    }

    public override Task RunAsync()
    {
        return Task.Run(async () =>
        {
            var producer = await Session.CreateProducerAsync(ReplierQueue);
            var consumer = await Session.CreateConsumerAsync(RequestorQueue);

            Logger($"The replier has been started");

            consumer.Listener += async message =>
            {
                var messageBody = message.Body<string>();
                Logger($"Replier receiving a message:\t{messageBody}");

                var replyMessageText = TextProcessor(messageBody);
                Logger($"Replier processes a message:\t{replyMessageText}");

                var replyMessage = await producer.CreateTextMessageAsync(replyMessageText);
                Logger($"Replier sends a reply:\t{replyMessageText}");

                await producer.SendAsync(replyMessage);
                await Task.Delay(DelayTime);
            };
        });
    }
}
