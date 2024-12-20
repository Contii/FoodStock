using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> CreateStock(StockModel stock)
        {
            if (stock.CategoryID.HasValue) // Verify if the stock has a value for category.
            {
                var category = await _context.Categories.FindAsync(stock.CategoryID.Value); // Verify if the category exists.
                if (category == null) return BadRequest("Invalid CategoryID"); // Return a 400 if the category does not exist.
            }

            stock.Items = new List<ItemModel>(); // Ensure the Items list is initialized as empty
            _context.Stocks.Add(stock); // Add the new stock to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetStock), new { id = stock.StockID }, stock); // Return the created stock.
        }

        // Update an existing stock.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, StockModel updatedStock)
        {
            var stock = await _context.Stocks.Include(s => s.Category).Include(s => s.Items).FirstOrDefaultAsync(s => s.StockID == id); // Verify if the stock exists.
            if (stock == null) return NotFound(); // Return a 404 if the stock does not exist.

            if (updatedStock.CategoryID.HasValue) // Verify if the updated stock has a value for category.
            {
                var category = await _context.Categories.FindAsync(updatedStock.CategoryID.Value); // Verify if the category exists.
                if (category == null) return BadRequest("Invalid CategoryID"); // Return a 400 if the category does not exist.
            }

            stock.Name = updatedStock.Name;
            stock.Description = updatedStock.Description;
            stock.Quantity = updatedStock.Quantity;
            stock.MinQuantity = updatedStock.MinQuantity;
            stock.MaxQuantity = updatedStock.MaxQuantity;
            stock.CategoryID = updatedStock.CategoryID;
            stock.Category = updatedStock.Category;
            stock.MeasureType = updatedStock.MeasureType;

            await _context.SaveChangesAsync(); // Save the changes to the database.
            return Ok(stock); // Return the updated stock.
        }

        // Delete a stock.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id); // Verify if the stock exists.
            if (stock == null) return NotFound(); // Return a 404 if the stock does not exist.

            _context.Stocks.Remove(stock); // Remove the stock from the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return NoContent(); // Return a 204.
        }
    }
}