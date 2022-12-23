using Newtonsoft.Json;
using TransactionService.Constants;
using TransactionService.Data.Repositories.Interfaces;
using TransactionService.Dtos;
using TransactionService.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TransactionService.EventProcessors;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;
    
    public EventProcessor(IServiceScopeFactory scopeFactory, ILogger<EventProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }
    
    public async Task ProcessEventAsync(string message)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);

        switch (eventType?.Event ?? "")
        {
            case MessageBusEvents.ItemCreated:
                await AddItemAsync(message);
                break;
            case MessageBusEvents.StorageCreated:
                await AddStorageAsync(message);
                break;
        }
    }

    private async Task AddItemAsync(string itemCreatedMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IItemRepository>();

            var itemPublishedDto = JsonSerializer.Deserialize<ItemPublishedDto>(itemCreatedMessage);
            if (itemPublishedDto is null)
            {
                _logger.LogInformation("Could not parse published item");
                return;
            }
           
            var item = new Item(itemPublishedDto);

            await repo.EnsureEntityExistsAsync(item);
            
            _logger.LogInformation("Published item was successfully saved");
        }
    }

    private async Task AddStorageAsync(string storageCreatedMessage)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var repo = scope.ServiceProvider.GetRequiredService<IStorageRepository>();

            var storagePublishedDto = JsonSerializer.Deserialize<StoragePublishedDto>(storageCreatedMessage);
            if (storagePublishedDto is null)
            {
                _logger.LogInformation("Could not parse published storage");
                return;
            }
           
            var storage = new Storage(storagePublishedDto);
            
            await repo.EnsureEntityExistsAsync(storage);
            
            _logger.LogInformation("Published storage was successfully saved");
        }
    }
    
}