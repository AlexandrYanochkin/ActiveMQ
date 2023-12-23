using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ActiveMQ.Common.Services.Base;

public abstract class BasePublishSubscribeClient : IDisposable
{
    private const string DefaultTopicName = "PublishSubscribe";

    private readonly IConnection _connection;

    protected ITopic Topic { get; }

    protected ISession Session { get; }

    public required Action<string> Logger { get; set; }

    protected BasePublishSubscribeClient(string brokerUri, string clientId, string? topicName)
    {
        var factory = new ConnectionFactory(brokerUri, clientId);

        _connection = factory.CreateConnection();
        Session = _connection.CreateSession();
        Topic = Session.GetTopic(topicName ?? DefaultTopicName);

        _connection.Start();
    }

    public abstract Task RunAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _connection.Stop();

        if (disposing)
        {
            Topic.Dispose();
            Session.Dispose();
            _connection.Dispose();
        }
    }

    ~BasePublishSubscribeClient()
    {
        Dispose(false);
    }
}