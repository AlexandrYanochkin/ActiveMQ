using ActiveMQ.Common.Helpers;
using ActiveMQ.Common.Services.PublishSubscribe.Subscribers;

internal class Program
{
    private const int CountOfSubscribers = 5;

    private static async Task Main(string[] args)
    {
        var brokerUri = ConfigurationHelper.GetBrokerUri();
        var subscriberTasks = Enumerable.Range(default, CountOfSubscribers)
            .Select(i => new VirtualSubscriberClient(brokerUri, $"subscriber[{i}]", $"Consumer.{i}.VirtualTopic.PrimaryTopic")
            { 
                Logger = Console.WriteLine,
            })
            .Select(subscriber => subscriber.RunAsync())
            .ToArray();

        await Task.WhenAll(subscriberTasks);

        Console.ReadLine();
    }
}