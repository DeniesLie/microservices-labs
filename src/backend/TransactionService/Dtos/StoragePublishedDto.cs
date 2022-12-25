using System;

namespace TransactionService.Dtos;

public class StoragePublishedDto 
{
    public Guid Id { get; set; }
    public string? Event { get; set; }
}