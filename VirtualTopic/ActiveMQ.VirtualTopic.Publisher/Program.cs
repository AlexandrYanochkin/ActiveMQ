using ActiveMQ.Common.Helpers;
using ActiveMQ.Common.Services.PublishSubscribe;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var brokerUri = ConfigurationHelper.GetBrokerUri();

        using var publisher = new PublisherClient(brokerUri, "Publisher", topicName: "VirtualTopic.PrimaryTopic")
        {
            Logger = Console.WriteLine,
            MessageInput = Console.ReadLine,
        };
        await publisher.RunAsync();

        Console.ReadLine();
    }
}