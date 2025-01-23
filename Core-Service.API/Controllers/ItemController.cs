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

            // Detach the existing stock to avoid tracking conflicts
            _context.Entry(stock).State = EntityState.Detached;
            item.Stock = null; // Set the stock to null to avoid re-adding it

            _context.Items.Add(item); // Add the new item to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.

            stock = await _context.Stocks.FindAsync(item.StockID); // retrieve the stock again to update.
            var stockController = new StockController(_context); // Create an instance of StockController.

            stock.Quantity += item.Measure; // Update the stock quantity
            var updateStockResult = await stockController.UpdateStock(stock.StockID, stock); // Call and await the UpdateStock method.

            if (updateStockResult is OkObjectResult okResult && okResult.Value is StockModel updatedStock)
            {
                stock = updatedStock;
            }
            else
            {
                return BadRequest("Failed to update stock");
            }
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

            _context.Entry(stock).State = EntityState.Unchanged; // Ensure the existing stock is used and not recreated
            var oldMeasure = item.Measure; // Store the old measure to update the stock quantity later.

            // === Update the item properties ===
            item.ItemDescription = updatedItem.ItemDescription;
            item.SpoilDate = updatedItem.SpoilDate;
            item.Measure = updatedItem.Measure;
            item.StockID = updatedItem.StockID;
            item.Stock = stock; // Ensure the existing stock is used

            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            // =================================

            // === Update the stock quantity ===
            stock.Quantity = stock.Quantity - oldMeasure + item.Measure; // Update the new stock quantity
            var updateStockResult = await new StockController(_context).UpdateStock(stock.StockID, stock); // Call and await the UpdateStock method.
            if (updateStockResult is OkObjectResult okResult && okResult.Value is StockModel updatedStock)
            {
                stock = updatedStock;
            }
            else
            {
                return BadRequest("Failed to update stock");
            }

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

            var stock = await _context.Stocks.FindAsync(item.StockID); // retrieve the stock to update.
            var stockController = new StockController(_context); // Create an instance of StockController.

            stock.Quantity -= item.Measure; // Update the stock quantity
            var updateStockResult = await stockController.UpdateStock(stock.StockID, stock); // Call and await the UpdateStock method.

            if (updateStockResult is OkObjectResult okResult && okResult.Value is StockModel updatedStock)
            {
                stock = updatedStock;
            }
            else
            {
                return BadRequest("Failed to update stock");
            }

            // Create a new Consumption entry
            try
            {
                var consumption = new ConsumptionModel
                {
                    Quantity = item.Measure,
                    ConsumptionDate = DateTime.UtcNow,
                    StockID = item.StockID,
                    Stock = stock
                };

                _context.Consumptions.Add(consumption);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create consumption: {ex.Message}");
            }

            return NoContent();
        }
    }
}