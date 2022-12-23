using StorageService.AsyncDataServices.Abstractions;
using StorageService.Dtos;

namespace StorageService.AsyncDataServices.Publishers;

public class MessageBusStoragePublisher : 
    MessageBusPublisher<StoragePublishedDto>, 
    IMessageBusPublisher<StoragePublishedDto>
{
    public MessageBusStoragePublisher(
        IConfiguration config, 
        ILogger<MessageBusClientBase<StoragePublishedDto>> logger) 
        : base(config, logger)
    {
    }
}