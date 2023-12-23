using ActiveMQ.Common.Helpers;
using ActiveMQ.Common.Services.PublishSubscribe;

namespace ActiveMQ.PublishSubscribe.Publisher;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var brokerUri = ConfigurationHelper.GetBrokerUri();

        using var publisher = new PublisherClient(brokerUri, "Publisher")
        {
            Logger = Console.WriteLine,
            MessageInput = Console.ReadLine,
        };
        await publisher.RunAsync();

        Console.ReadLine();
    }
}
