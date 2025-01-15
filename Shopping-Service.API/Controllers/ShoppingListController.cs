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
    public class ShoppingListController : ControllerBase
    {
        private readonly EFCoreContext _context; // The context is used to interact with the database.

        public ShoppingListController(EFCoreContext context) // The context is injected in the controller.
        {
            _context = context;
        }

        // Obtaining all shopping lists.
        [HttpGet]
        public async Task<IActionResult> GetShoppingLists()
        {
            return Ok(await _context.ShoppingLists.ToListAsync()); // Return all shopping lists from the database.
        }

        // Obtaining a specific shoppingList by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingList(int id)
        {
            var shoppingList = await _context.ShoppingLists.FindAsync(id); // Verify if the shoppingList exists.
            return shoppingList is not null ? Ok(shoppingList) : NotFound(); // Return the shoppingList if it exists, otherwise return a 404.
        }

        // Create a new shoppingList.
        [HttpPost]
        public async Task<IActionResult> CreateShoppingList([FromBody] ShoppingListModel shoppingList)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            // if the ShoppingItens is not empty or null, verify if all the items in the new shopping list are already in the database
            if (shoppingList.ShoppingItens != null && shoppingList.ShoppingItens.Count > 0)
            {
                foreach (var item in shoppingList.ShoppingItens)
                {
                    var stock = await _context.Stocks.FindAsync(item.StockID);
                    // if the stock is not found, return 404 with a list of all the StockIDs that was not found
                    if (stock == null) return NotFound(new { item.StockID });
                }
            }

            _context.ShoppingLists.Add(shoppingList); // Add the new shoppingList to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetShoppingList), new { id = shoppingList.ShoppingListID }, shoppingList);
        }

        // Update an existing shoppingList.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingList(int id, [FromBody] ShoppingListModel updatedShoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shoppingList = await _context.ShoppingLists.FindAsync(id); // Verify if the shoppingList exists.
            if (shoppingList == null) return NotFound();

            shoppingList.Name = updatedShoppingList.Name;
            shoppingList.ShoppingDate = updatedShoppingList.ShoppingDate;
            shoppingList.Status = updatedShoppingList.Status;

            // if the ShoppingItens is not empty or null, verify if all the items in the new shopping list are already in the database
            if (shoppingList.ShoppingItens != null && shoppingList.ShoppingItens.Count > 0)
            {
                foreach (var item in shoppingList.ShoppingItens)
                {
                    var stock = await _context.Stocks.FindAsync(item.StockID);
                    // if the stock is not found, return 404 with a list of all the StockIDs that was not found
                    if (stock == null) return NotFound(new { item.StockID });
                }
            }
            
            shoppingList.ShoppingItens = updatedShoppingList.ShoppingItens;

            await _context.SaveChangesAsync(); 
            return Ok(shoppingList);
        }

        // Delete a shoppingList.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingList(int id)
        {
            var shoppingList = await _context.ShoppingLists.FindAsync(id); // Verify if the shoppingList exists.
            if (shoppingList == null) return NotFound();

            _context.ShoppingLists.Remove(shoppingList); // Remove the shoppingList from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}