using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Core_Service.API.Controllers
{
    [ApiController] // Activates the class in an API controller design.
    [Route("api/[controller]")] // The route will be the controller name, supressing "Controller" from it.
    public class CategoryController : ControllerBase
    {
        private readonly EFCoreContext _context; // The context is used to interact with the database.

        public CategoryController(EFCoreContext context) // The context is injected in the controller.
        {
            _context = context;
        }

        // Obtaining all categories.
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _context.Categories.ToListAsync()); // Return all categories from the database.
        }

        // Obtaining a specific category by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id); // Verify if the category exists.
            return category is not null ? Ok(category) : NotFound(); // Return the category if it exists, otherwise return a 404.
        }

        // Create a new category.
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel category)
        {
            _context.Categories.Add(category); // Add the new category to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryID }, category); // Return the created category.
        }

        // Update an existing category.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel updatedCategory)
        {
            var category = await _context.Categories.FindAsync(id); // Verify if the category exists.
            if (category == null) return NotFound(); // Return a 404 if the category does not exist.

            category.Name = updatedCategory.Name; // Update the category name.
            category.Description = updatedCategory.Description; // Update the category description.

            await _context.SaveChangesAsync(); // Save the changes to the database.
            return Ok(category); // Return the updated category.
        }

        // Delete a category.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id); // Verify if the category exists.
            if (category == null) return NotFound(); // Return a 404 if the category does not exist.

            _context.Categories.Remove(category); // Remove the category from the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return NoContent(); // Return a 204.
        }
    }
}