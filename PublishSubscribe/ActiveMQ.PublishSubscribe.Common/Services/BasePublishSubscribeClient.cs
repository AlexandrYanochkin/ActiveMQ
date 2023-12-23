using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ActiveMQ.PublishSubscribe.Common.Services;

public abstract class BasePublishSubscribeClient : IDisposable
{
    private const string DefaultTopicName = "PublishSubscribe";

    private readonly IConnection _connection;

    protected ITopic Topic { get; }

    protected ISession Session { get; }

    protected Action<string> Logger { get; }

    protected BasePublishSubscribeClient(string brokerUri, string clientId, string topicName, Action<string> logger)
    {
        var factory = new ConnectionFactory(brokerUri, clientId);

        _connection = factory.CreateConnection();
        Session = _connection.CreateSession();
        Topic = Session.GetTopic(topicName ?? DefaultTopicName);
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
