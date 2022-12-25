using System;

namespace StorageService.Dtos;

public class StoragePublishedDto 
{
    public Guid Id { get; set; }
    public string? Event { get; set; }

    public StoragePublishedDto(Guid id, string storageEvent)
    {
        Id = id;
        Event = storageEvent;
    }
}