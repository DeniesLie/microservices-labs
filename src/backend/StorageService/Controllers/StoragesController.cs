using Microsoft.AspNetCore.Mvc;
using StorageService.Dtos;
using Microsoft.EntityFrameworkCore;
using StorageService.AsyncDataServices.Abstractions;
using StorageService.Constants;
using StorageService.Models;
using StorageService.Repositories.Interfaces;

namespace StorageService.Controllers;

[ApiController]
[Route("api/storages")]
public class StoragesController : ControllerBase
{
    private readonly IStorageRepository _storageRepository;
    private readonly HealthState _healthState;
    private readonly IMessageBusPublisher<StoragePublishedDto> _messageBusPublisher;

    public StoragesController(
        IStorageRepository storageRepository, 
        HealthState healthState, 
        IMessageBusPublisher<StoragePublishedDto> messageBusPublisher)
    {
        _storageRepository = storageRepository;
        _healthState = healthState;
        _messageBusPublisher = messageBusPublisher;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StorageGetDto>>> GetAll()
    {
        return await _storageRepository.Query()
            .Select(s => new StorageGetDto(s))
            .ToListAsync();
    }

    [HttpGet("{storageId:guid}", Name = "GetById")]
    public async Task<ActionResult<StorageGetDto>> GetByIdAsync(Guid storageId)
    {
        var storage = await _storageRepository.GetByIdAsync(storageId);

        if (storage is null) return NotFound();

        return new StorageGetDto(storage);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<StorageGetDto>> CreateAsync([FromBody] StoragePostDto storagePostDto)
    {
        var storage = new Storage(storagePostDto);

        _storageRepository.Create(storage);
        await _storageRepository.SaveChangesAsync();

        var publishedDto = new StoragePublishedDto(storage.Id, MessageBusEvents.Created);
        _messageBusPublisher.Publish(publishedDto, "storage.created");
        
        var result = new StorageGetDto(storage);
        return CreatedAtRoute("GetById", new { storageId = result.Id }, result);
    }

    [HttpPut("Update")]
    public async Task<ActionResult<StorageGetDto>> UpdateAsync([FromBody] StorageUpdateDto storageUpdateDto)
    {
        var storage = await _storageRepository.GetByIdAsync(storageUpdateDto.Id);

        if (storage is null) return NotFound();

        storage.Name = storageUpdateDto.Name;
        storage.Address = storageUpdateDto.Address;
        _storageRepository.Update(storage);
        await _storageRepository.SaveChangesAsync();

        return new StorageGetDto(storage);
    }

    [HttpDelete("{storageId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid storageId)
    {
        var storage = await _storageRepository.GetByIdAsync(storageId);

        if (storage is null) return NotFound();

        _storageRepository.Delete(storage);
        await _storageRepository.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("brokenEndpoint")]
    public IActionResult BrokenEndpoint()
    {
        _healthState.IsSlowed = true;
        return Ok("Service was successfully broken");
    }

    [HttpGet("speedTest")]
    public IActionResult SpeedTest()
    {
        if (_healthState.IsSlowed)
            Thread.Sleep(10000);

        return Ok();
    }
}
