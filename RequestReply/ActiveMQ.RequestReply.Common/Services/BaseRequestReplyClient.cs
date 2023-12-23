using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ActiveMQ.RequestReply.Common.Services;

public abstract class BaseRequestReplyClient : IDisposable
{
    protected const int DelayTime = 2000;

    private readonly IConnection _connection;

    protected ISession Session { get; }

    protected IQueue RequestorQueue { get; }

    protected IQueue ReplierQueue { get; }

    public required Action<string> Logger { get; set; }

    protected BaseRequestReplyClient(string brokerUri)
    {
        var factory = new ConnectionFactory(brokerUri);

        _connection = factory.CreateConnection();
        Session = _connection.CreateSession();
        RequestorQueue = Session.GetQueue(nameof(RequestorQueue));
        ReplierQueue = Session.GetQueue(nameof(ReplierQueue));

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
            RequestorQueue.Dispose();
            ReplierQueue.Dispose();
            Session.Dispose();
            _connection.Dispose();
        }
    }

    ~BaseRequestReplyClient() 
    {
        Dispose(false);
    }
}
