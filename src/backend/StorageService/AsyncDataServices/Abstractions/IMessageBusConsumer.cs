namespace StorageService.AsyncDataServices.Abstractions;

public interface IMessageBusConsumer
{
    void SubscribeOn(string topic);
}