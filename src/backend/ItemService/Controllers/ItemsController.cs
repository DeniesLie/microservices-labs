using ItemService.AsyncDataServices.Abstractions;
using ItemService.Constants;
using ItemService.DTOs;
using ItemService.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers;

[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IMessageBusPublisher<ItemPublishedDto> _messageBus;
    private readonly ILogger _logger;

    public ItemsController(IItemService itemService, IMessageBusPublisher<ItemPublishedDto> messageBus, ILogger<ItemsController> logger)
    {
        _itemService = itemService;
        _messageBus = messageBus;
        _logger = logger;
    }
    
        [HttpGet("{itemId:guid}", Name = "GetById")]
        public async Task<ActionResult<ItemGetDto>> GetByIdAsync(Guid itemId)
        {
            try { return await _itemService.GetByIdAsync(itemId); }
            catch (ArgumentException) { return NotFound(); }
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ItemGetDto>>> GetAllAsync()
        {
            return await _itemService.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ItemGetDto>> CreateAsync([FromBody] ItemPostDto itemPostDto)
        {
            var createdItem = await _itemService.CreateAsync(itemPostDto);
            _logger.LogInformation($"Item created. Id: {createdItem.Id}");

            var publisDto = new ItemPublishedDto(createdItem) { Event = MessageBusEvents.Created };
            _messageBus.Publish(publisDto, "item");

            return CreatedAtRoute("GetById", 
                new { itemId = createdItem.Id }, 
                createdItem);
        }

        [HttpPut]
        public async Task<ActionResult<ItemGetDto>> UpdateAsync([FromBody] ItemPutDto itemPutDto)
        {
            try 
            { 
                return await _itemService.UpdateAsync(itemPutDto);
                _logger.LogInformation($"Item updated");
            }
            catch (ArgumentException)
            {
                return NotFound(); 
            }
        }

        [HttpDelete("{itemId:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid itemId)
        {
            try
            {
                await _itemService.DeleteAsync(itemId);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
}