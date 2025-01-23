using FoodStock.Models;
using FoodStock.Persistence;
using FoodStock.Persistence.Observers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodStock.Core_Service.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly EFCoreContext _context;

        public StockController(EFCoreContext context)
        {
            _context = context;
        }

        // Obtaining all stocks.
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            return Ok(await _context.Stocks.Include(s => s.Category).Include(s => s.Items).ToListAsync()); // Return all stocks from the database, including the category and items.
        }

        // Obtaining a specific stock by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock(int id)
        {
            var stock = await _context.Stocks.Include(s => s.Category).Include(s => s.Items).FirstOrDefaultAsync(s => s.StockID == id); // Verify if the stock exists and include related category and items.
            return stock is not null ? Ok(stock) : NotFound(); // Return the stock if it exists, otherwise return a 404.

        }

        // Create a new stock.
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockModel stock)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            if (stock.CategoryID.HasValue) // Verify if the stock has a value for category.
            {
                var category = await _context.Categories.FindAsync(stock.CategoryID.Value); // Verify if the category exists.
                if (category == null) return BadRequest("Invalid CategoryID");

                // Detach the existing category to avoid tracking conflicts
                _context.Entry(category).State = EntityState.Detached;
                stock.Category = null; // Set the category to null to avoid re-adding it
            }

            stock.Items = new List<ItemModel>(); // Ensure the Items list is initialized as empty
            _context.Stocks.Add(stock); // Add the new stock to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.

            // ===== Notify the observer with the new stock state =====
            var stockObserver = new StockObserver(_context); // Create an instance of StockObserver.
            stock.Attach(stockObserver); // Attach the observer to the stock.
            stock.Notify(stock.Quantity); // Notify the observer with the new stock state
            await _context.SaveChangesAsync(); // Save the changes to the database.
            // ========================================================

            return CreatedAtAction(nameof(GetStock), new { id = stock.StockID }, stock);
        }

        // Update an existing stock.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] StockModel updatedStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _context.Stocks.Include(s => s.Category).Include(s => s.Items).FirstOrDefaultAsync(s => s.StockID == id); // Verify if the stock exists.
            if (stock == null) return NotFound();

            // Store the old quantity to notify the observer
            var oldQuantity = stock.Quantity;

            if (updatedStock.CategoryID.HasValue)
            {
                var category = await _context.Categories.FindAsync(updatedStock.CategoryID.Value); // Verify if the category exists.
                if (category == null) return BadRequest("Invalid CategoryID");

                // Detach the existing category to avoid tracking conflicts
                _context.Entry(category).State = EntityState.Detached;
                stock.Category = null; // Set the category to null to avoid re-adding it

                stock.CategoryID = updatedStock.CategoryID;
                stock.Category = updatedStock.Category;
            }

            stock.Name = updatedStock.Name;
            stock.Description = updatedStock.Description;
            stock.Quantity = updatedStock.Quantity;
            stock.MinQuantity = updatedStock.MinQuantity;
            stock.MaxQuantity = updatedStock.MaxQuantity;
            stock.MeasureType = updatedStock.MeasureType;

            await _context.SaveChangesAsync(); // Save the changes before notifying observer.

            // ===== Notify the observer with the old and new stock state =====
            var stockObserver = new StockObserver(_context); // Create an instance of StockObserver.
            stock.Attach(stockObserver); // Attach the observer to the stock.
            stock.Notify(oldQuantity); // Notify the observer with the old stock state
            // ========================================================

            return Ok(stock);
        }

        // Delete a stock.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id); // Verify if the stock exists.
            if (stock == null) return NotFound();

            _context.Stocks.Remove(stock); // Remove the stock from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}