using ActiveMQ.Common.Helpers;
using ActiveMQ.Common.Services.PublishSubscribe.Subscribers;

namespace ActiveMQ.PublishSubscribe.DurableSubscriber;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var brokerUri = ConfigurationHelper.GetBrokerUri();

        using var subscriber = new DurableSubscriberClient(brokerUri, "DurableSubscriber")
        {
            Logger = Console.WriteLine,
        };
        await subscriber.RunAsync();

        Console.ReadLine();
    }
}