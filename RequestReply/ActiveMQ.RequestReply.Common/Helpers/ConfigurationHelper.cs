using Microsoft.Extensions.Configuration;

namespace ActiveMQ.RequestReply.Common.Helpers;

public static class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new[] 
            { 
                new KeyValuePair<string, string>("brokerUri", "tcp://localhost:61616?wireFormat.maxInactivityDuration=50000")
            })
            .Build();
    }
}
