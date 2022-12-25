using System;

namespace TransactionService.Dtos;

public class ItemPublishedDto 
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Event { get; set; }
}