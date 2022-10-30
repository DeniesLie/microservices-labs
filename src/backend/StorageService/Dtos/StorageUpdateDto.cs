using StorageService.Models;

namespace StorageService.Dtos;

public class StorageUpdateDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }
}
