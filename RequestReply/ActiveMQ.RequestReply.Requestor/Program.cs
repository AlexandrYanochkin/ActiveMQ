using ActiveMQ.Common.Helpers;
using ActiveMQ.Common.Services.RequestReply;

namespace ActiveMQ.RequestReply.Requestor;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var brokerUri = ConfigurationHelper.GetBrokerUri();

        using var requestor = new RequestorClient(brokerUri)
        {
            Logger = Console.WriteLine,
            CreateRandomMessage = TextHelper.CreateRandomText,
            TextMessageLength = TextHelper.DefaultTextMessageLength,
        };

        await requestor.RunAsync();

        Console.ReadKey();
    }
}