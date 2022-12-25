using System.Text;
using System.Text.Json;
using StorageService.AsyncDataServices.Abstractions;

namespace StorageService.AsyncDataServices.Publishers;

public class MessageBusPublisher<TMessage> : 
    MessageBusClientBase<TMessage>, 
    IMessageBusPublisher<TMessage>
{
    public MessageBusPublisher(
        IConfiguration config, 
        ILogger<MessageBusClientBase<TMessage>> logger) 
        : base(config, logger)
    {
        
    }

    public void Publish(TMessage message, string routingKey)
    {
        var jsonMessage = JsonSerializer.Serialize(message);

        if (Connection.IsOpen)
            SendMessage(jsonMessage, routingKey);
    }

    private void SendMessage(string message, string routingKey)
    {
        var body = Encoding.UTF8.GetBytes(message);

        try
        {
            Chanel.BasicPublish(exchange: ExchangeName,
                routingKey: routingKey,
                basicProperties: null,
                mandatory: true,
                body: body);
        }
        catch (Exception e)
        {
            Logger.LogError($"Failed to publish message to {RabbitMqHost}:{RabbitMqPort}");
        }

        Logger.LogInformation($"Message {message} published to {RabbitMqHost}:{RabbitMqPort}");
    }
}