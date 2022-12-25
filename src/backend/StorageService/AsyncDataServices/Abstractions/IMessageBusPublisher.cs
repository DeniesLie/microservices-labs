namespace StorageService.AsyncDataServices.Abstractions;

public interface IMessageBusPublisher<TEntity>
{
    void Publish(TEntity entity, string routingKey);
}