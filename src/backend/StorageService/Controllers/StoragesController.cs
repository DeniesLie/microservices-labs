using Microsoft.AspNetCore.Mvc;
using StorageService.Dtos;

namespace StorageService.Controllers;

[ApiController]
[Route("api/storages")]
public class StoragesController : ControllerBase
{
    [HttpGet]
    public IEnumerable<StorageGetDto> GetAll()
    {
        return new List<StorageGetDto>()
        {
            new StorageGetDto()
            {
                Id = 1,
                Name = "Storage A",
                Address = "Winners avenue, 10"
            },
            new StorageGetDto()
            {
                Id = 2,
                Name = "Storage B",
                Address = "Magic street, 70/2"
            },
            new StorageGetDto()
            {
                Id = 3,
                Name = "Storage C",
                Address = "Central square"
            }
        };
    }

    [HttpGet("{storageId:int:min(1)}")]
    public StorageGetDto GetById(int storageId)
    {
        return new StorageGetDto()
        {
            Id = 3,
            Name = "Storage C",
            Address = "Central square"
        };
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] StoragePostDto storagePostDto)
    {
        return Ok(new StorageGetDto()
        {
            Id = 4,
            Name = "Storage D",
            Address = "Proscura's street, 12"
        });
    }
}
