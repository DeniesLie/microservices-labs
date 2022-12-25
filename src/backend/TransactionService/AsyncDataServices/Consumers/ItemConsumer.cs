using TransactionService.AsyncDataServices.Abstractions;
using TransactionService.Dtos;

namespace TransactionService.AsyncDataServices.Consumers;

public class ItemConsumer : MessageBusConsumer<ItemPublishedDto>
{
    public ItemConsumer(
        IConfiguration config, 
        ILogger<MessageBusClientBase<ItemPublishedDto>> logger) 
        : base(config, logger, "item_queue", "item")
    {
    }
}