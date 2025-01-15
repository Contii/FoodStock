using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Shopping_Service.API.Controllers
{
    [ApiController] // Activates the class in an API controller design.
    [Route("api/[controller]")] // The route will be the controller name, supressing "Controller" from it.
    [EnableCors("AllowAll")] // Enable CORS for the controller.
    public class ShoppingItemController : ControllerBase
    {
        private readonly EFCoreContext _context; // The context is used to interact with the database.

        public ShoppingItemController(EFCoreContext context) // The context is injected in the controller.
        {
            _context = context;
        }

        // Obtaining all shopping itens.
        [HttpGet]
        public async Task<IActionResult> GetShoppingItens()
        {
            return Ok(await _context.ShoppingItens.ToListAsync()); // Return all shopping itens from the database.
        }

        // Obtaining a specific shopping item by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItens.FindAsync(id); // Verify if the shopping item exists.
            return shoppingItem is not null ? Ok(shoppingItem) : NotFound(); // Return the shopping item if it exists, otherwise return a 404.
        }

        // Create a new shopping item.
        [HttpPost]
        public async Task<IActionResult> CreateShoppingItem([FromBody] ShoppingItemModel shoppingItem)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            var stock = await _context.Stocks.FindAsync(shoppingItem.StockID); // Verify if the stock exists.
            if (stock == null) return NotFound(new { shoppingItem.StockID });
            _context.Entry(stock).State = EntityState.Unchanged; // Attach the existing Stock to the context to avoid creating a new one
            shoppingItem.Stock = stock;

            var shoppingList = await _context.ShoppingLists.FindAsync(shoppingItem.ShoppingListID); // Verify if the shopping list exists.
            if (shoppingList == null) return NotFound(new { shoppingItem.ShoppingListID });
            _context.Entry(shoppingList).State = EntityState.Unchanged;
            shoppingItem.ShoppingList = shoppingList;

            _context.ShoppingItens.Add(shoppingItem); // Add the new shopping item to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetShoppingItem), new { id = shoppingItem.ShoppingItemID }, shoppingItem);
        }

        // Update an existing shopping item.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingItem(int id, [FromBody] ShoppingItemModel updatedShoppingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingItem = await _context.ShoppingItens.FindAsync(id); // Verify if the shopping item exists.
            if (shoppingItem == null) return NotFound();
            
            var stock = await _context.Stocks.FindAsync(updatedShoppingItem.StockID); // Verify if the stock exists.
            if (stock == null) return NotFound(new { updatedShoppingItem.StockID });
            _context.Entry(stock).State = EntityState.Unchanged; // Attach the existing Stock to the context to avoid creating a new one

            var shoppingList = await _context.ShoppingLists.FindAsync(updatedShoppingItem.ShoppingListID); // Verify if the shopping list exists.
            if (shoppingList == null) return NotFound(new { updatedShoppingItem.ShoppingListID });
            _context.Entry(shoppingList).State = EntityState.Unchanged;

            shoppingItem.Measure = updatedShoppingItem.Measure;
            shoppingItem.StockID = updatedShoppingItem.StockID;
            shoppingItem.Stock = stock;
            shoppingItem.ShoppingListID = updatedShoppingItem.ShoppingListID;
            shoppingItem.ShoppingList = shoppingList;

            await _context.SaveChangesAsync(); 
            return Ok(shoppingItem);
        }

        // Delete a shopping item.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItens.FindAsync(id); // Verify if the shopping item exists.
            if (shoppingItem == null) return NotFound();

            _context.ShoppingItens.Remove(shoppingItem); // Remove the shopping item from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}