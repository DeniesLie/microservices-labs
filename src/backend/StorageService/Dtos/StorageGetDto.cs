using StorageService.Models;

namespace StorageService.Dtos;

public class StorageGetDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public StorageGetDto() {}

    public StorageGetDto(Storage storage)
    {
        Id = storage.Id;
        Name = storage.Name;
        Address = storage.Address;
    }
}
