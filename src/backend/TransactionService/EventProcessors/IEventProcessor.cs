namespace TransactionService.EventProcessors;

public interface IEventProcessor
{
    Task ProcessEventAsync(string message);
}