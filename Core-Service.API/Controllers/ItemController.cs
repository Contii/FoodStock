using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Core_Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly EFCoreContext _context;

        public ItemController(EFCoreContext context)
        {
            _context = context;
        }

        // Obtaining all items.
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            return Ok(await _context.Items.Include(i => i.Stock).ToListAsync()); // Return all items from the database, including the related stock.
        }

        // Obtaining a specific item by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id) 
        {
            var item = await _context.Items.Include(i => i.Stock).FirstOrDefaultAsync(i => i.ItemID == id); // Verify if the item exists and include the related stock info in the context.
            return item is not null ? Ok(item) : NotFound(); // Return the item if it exists, otherwise return a 404.
        }

        // Create a new item.
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemModel item)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            var stock = await _context.Stocks.FindAsync(item.StockID); // Verify if the stock exists.
            if (stock == null) return BadRequest("Invalid StockID");

            _context.Items.Add(item); // Add the new item to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetItem), new { id = item.ItemID }, item);
        }

        // Update an existing item.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemModel updatedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.Include(i => i.Stock).FirstOrDefaultAsync(i => i.ItemID == id); // Verify if the item exists.
            if (item == null) return NotFound();

            var stock = await _context.Stocks.FindAsync(updatedItem.StockID); // Verify if the stock exists.
            if (stock == null) return BadRequest("Invalid StockID");

            item.ItemDescription = updatedItem.ItemDescription;
            item.SpoilDate = updatedItem.SpoilDate;
            item.Measure = updatedItem.Measure;
            item.StockID = updatedItem.StockID;
            item.Stock = updatedItem.Stock;

            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // Delete an item.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id); // Verify if the item exists.
            if (item == null) return NotFound();

            _context.Items.Remove(item); // Remove the item from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}