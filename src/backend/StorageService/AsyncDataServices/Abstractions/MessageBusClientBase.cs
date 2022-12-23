using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace StorageService.AsyncDataServices.Abstractions;

public abstract class MessageBusClientBase<TMessage>: IDisposable
{
    protected readonly string RabbitMqHost;
    protected readonly int RabbitMqPort;
    
    protected readonly IConfiguration Config;
    protected readonly ILogger Logger;

    protected readonly IConnection Connection;
    protected readonly IModel Chanel;
    
    protected readonly string ExchangeName;
    
    public MessageBusClientBase(
        IConfiguration config, 
        ILogger<MessageBusClientBase<TMessage>> logger)
    {
        Config = config;
        Logger = logger;
        
        ExchangeName = Config["Services:RabbitMQ:ExchangeName"];

        RabbitMqHost = Config["Services:RabbitMQ:Host"];
        RabbitMqPort = int.Parse(Config["Services:RabbitMQ:Port"]);
        
        var connFactory = new ConnectionFactory()
        {
            HostName = RabbitMqHost,
            Port = RabbitMqPort,
            ContinuationTimeout = TimeSpan.FromMilliseconds(20000),
            RequestedConnectionTimeout = TimeSpan.FromMilliseconds(20000),
            UserName = "dev",
            Password = "3hE769Cna9mW"
        };

        try
        {
            Connection = connFactory.CreateConnection();
            Chanel = Connection.CreateModel();
            
            Chanel.ExchangeDeclare(ExchangeName, type: ExchangeType.Topic);

            Connection.ConnectionShutdown += HandleConnectionShutdown;
            
            Logger.LogInformation($"Successfully connected to RabbitMQ at {RabbitMqHost}:{RabbitMqPort}");
        }
        catch (Exception e)
        {
            Logger.LogError($"Could not connect to RabbitMQ at {RabbitMqHost}:{RabbitMqPort}");
            throw;
        }
    }
    
    private void HandleConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        Logger.LogInformation("RabbitMQ connection shutdown");
    }

    public void Dispose()
    {
        Chanel.Dispose();
        Connection.Dispose();
    }
}