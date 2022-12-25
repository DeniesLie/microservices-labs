using ItemService.AsyncDataServices.Abstractions;
using ItemService.DTOs;

namespace ItemService.AsyncDataServices.Publishers;

public class MessageBusItemPublisher : 
    MessageBusPublisher<ItemPublishedDto>, 
    IMessageBusPublisher<ItemPublishedDto>
{
    public MessageBusItemPublisher(
        IConfiguration config, 
        ILogger<MessageBusClientBase<ItemPublishedDto>> logger) 
        : base(config, logger)
    {
    }
}