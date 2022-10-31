using ItemService.DTOs;
using ItemService.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers;

[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;
    
    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
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

            return CreatedAtRoute("GetById", 
                new { itemId = createdItem.Id }, 
                createdItem);
        }

        [HttpPut]
        public async Task<ActionResult<ItemGetDto>> UpdateAsync([FromBody] ItemPutDto itemPutDto)
        {
            try { return await _itemService.UpdateAsync(itemPutDto); }
            catch (ArgumentException) { return NotFound(); }
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