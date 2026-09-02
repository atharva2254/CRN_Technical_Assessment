using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRN_Technical_Assessment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/items
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _itemService.GetAllItems();

            return Ok(items);
        }

        // GET: api/items/id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _itemService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // GET: api/items/product/id
        [HttpGet("product/{productId:int}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var items = await _itemService.GetByProductsId(productId);

            return Ok(items);
        }

        // POST: api/items
        [HttpPost]
        public async Task<IActionResult> Create(ItemDto request)
        {
            var item = await _itemService.CreateItem(request);

            if (item == null)
                return NotFound($"Product with ID {request.ProductId} was not found.");

            return CreatedAtAction(
                nameof(GetById),
                new { id = item.Id },
                item);
        }

        // PUT: api/items/id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,ItemDto request)
        {
            var item = await _itemService.UpdateItem(id, request);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // DELETE: api/items/id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _itemService.DeleteItem(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}