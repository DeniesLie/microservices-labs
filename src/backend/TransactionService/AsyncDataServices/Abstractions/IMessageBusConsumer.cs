using RabbitMQ.Client.Events;

namespace TransactionService.AsyncDataServices.Abstractions;

public interface IMessageBusConsumer<TMessage> : IDisposable
{
    void Consume();
    EventingBasicConsumer Consumer { get;  }
}