using System.Text;
using TransactionService.AsyncDataServices.Abstractions;
using TransactionService.Dtos;
using TransactionService.EventProcessors;

namespace TransactionService.BackgroundServices;

public class MessageBusListener : BackgroundService
{
    private readonly IMessageBusConsumer<ItemPublishedDto> _itemConsumer;
    private readonly IMessageBusConsumer<StoragePublishedDto> _storageConsumer;
    private readonly IEventProcessor _eventProcessor;
    private readonly ILogger _logger;
    
    public MessageBusListener(
        IMessageBusConsumer<ItemPublishedDto> itemConsumer, 
        IMessageBusConsumer<StoragePublishedDto> storageConsumer, 
        IEventProcessor eventProcessor,
        ILogger<MessageBusListener> logger)
    {
        _itemConsumer = itemConsumer;
        _storageConsumer = storageConsumer;
        _eventProcessor = eventProcessor;
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Starting message bus listener service");
        
        _itemConsumer.Consumer.Received += async (ModuleHandle, ea) =>
        {
            _logger.LogInformation("Received message from item service");
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            await _eventProcessor.ProcessEventAsync(message);
        };
        
        _storageConsumer.Consumer.Received += async (ModuleHandle, ea) =>
        {
            _logger.LogInformation("Received message from storage service");
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            await _eventProcessor.ProcessEventAsync(message);
        };

        _itemConsumer.Consume();
        _storageConsumer.Consume();

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _itemConsumer.Dispose();
        _storageConsumer.Dispose();
        base.Dispose();
    }
}