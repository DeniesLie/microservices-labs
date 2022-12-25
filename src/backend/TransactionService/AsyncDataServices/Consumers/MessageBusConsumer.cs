using RabbitMQ.Client.Events;
using TransactionService.AsyncDataServices.Abstractions;

namespace TransactionService.AsyncDataServices.Consumers;

public class MessageBusConsumer<TMessage> : 
    MessageBusClientBase<TMessage>,
    IMessageBusConsumer<TMessage>
    
{
    protected readonly string QueueName;
    protected readonly string BindingKey;

    private readonly EventingBasicConsumer _consumer;
    
    public MessageBusConsumer(
        IConfiguration config, 
        ILogger<MessageBusClientBase<TMessage>> logger,
        string queueName,
        string bindingKey) 
        : base(config, logger)
    {
        QueueName = queueName;
        BindingKey = bindingKey;
        
        Chanel.QueueDeclare(QueueName, 
            true, 
            false, 
            true, 
            null);
        
        object queueTypeValue = "classic";
        
        Chanel.QueueBind(queue: QueueName,
            exchange: ExchangeName,
            routingKey: bindingKey,
            new Dictionary<string, object>()
            {
                {"x-queue-type", queueTypeValue}
            });

        _consumer = new EventingBasicConsumer(Chanel);
        _consumer.ConsumerTag = Guid.NewGuid().ToString();
    }

    public void Consume()
    {
        Chanel.BasicConsume(QueueName, 
            autoAck: true, 
            consumerTag: _consumer.ConsumerTag, 
            noLocal: true, 
            exclusive: false, 
            null, 
            consumer: _consumer);
    }
    
    public EventingBasicConsumer Consumer
    {
        get => _consumer;
    }
    
    
}