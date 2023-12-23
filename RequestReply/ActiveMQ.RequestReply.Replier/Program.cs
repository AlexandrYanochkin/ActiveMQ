using ActiveMQ.Common.Helpers;
using ActiveMQ.Common.Services.RequestReply;

namespace ActiveMQ.RequestReply.Replier;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var brokerUri = ConfigurationHelper.GetBrokerUri();

        using var replier = new ReplierClient(brokerUri) 
        {
            Logger = Console.WriteLine,
            TextProcessor = TextHelper.ReverseText,
        };

        await replier.RunAsync();

        Console.ReadKey();
    }
}