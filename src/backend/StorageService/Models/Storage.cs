using StorageService.Dtos;

namespace StorageService.Models;

public class Storage: Model
{
    public string? Name { get; set; }

	public string? Address { get; set; }

    public Storage() {}

    public Storage(StoragePostDto dto)
    {
        Name = dto.Name;
        Address = dto.Address;
    }
}
