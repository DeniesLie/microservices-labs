using TransactionService.AsyncDataServices.Abstractions;
using TransactionService.Dtos;

namespace TransactionService.AsyncDataServices.Consumers;

public class StorageConsumer : MessageBusConsumer<StoragePublishedDto>
{
    public StorageConsumer(
        IConfiguration config, 
        ILogger<MessageBusClientBase<StoragePublishedDto>> logger) 
        : base(config, logger, "storage_queue", "storage")
    {
    }
}