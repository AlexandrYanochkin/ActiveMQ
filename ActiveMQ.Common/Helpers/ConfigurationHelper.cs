using Microsoft.Extensions.Configuration;

namespace ActiveMQ.Common.Helpers;

public static class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("brokerUri", "tcp://localhost:61616?wireFormat.maxInactivityDuration=50000")
            })
            .Build();
    }

    public static string GetBrokerUri()
    {
        var configuration = GetConfiguration();
        
        return configuration["brokerUri"] ?? throw new ArgumentNullException("brokerUri");
    }
}
