using ActiveMQ.Common.Helpers;
using ActiveMQ.Common.Services.PublishSubscribe.Subscribers;

namespace ActiveMQ.PublishSubscribe.NonDurableSubscriber;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var brokerUri = ConfigurationHelper.GetBrokerUri();

        using var subscriber = new NonDurableSubscriberClient(brokerUri, "NonDurableSubscriber")
        {
            Logger = Console.WriteLine,
        };
        await subscriber.RunAsync();

        Console.ReadLine();
    }
}
