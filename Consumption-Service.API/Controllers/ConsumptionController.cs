using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Consumption_Service.API.Controllers
{
    [ApiController] // Activates the class in an API controller design.
    [Route("api/[controller]")] // The route will be the controller name, supressing "Controller" from it.
    [EnableCors("AllowAll")] // Enable CORS for the controller.
    public class ConsumptionController : ControllerBase
    {
        private readonly EFCoreContext _context; // The context is used to interact with the database.

        public ConsumptionController(EFCoreContext context) // The context is injected in the controller.
        {
            _context = context;
        }

        // Obtaining all consumptions.
        [HttpGet]
        public async Task<IActionResult> GetConsumptions()
        {
            return Ok(await _context.Consumptions.ToListAsync()); // Return all consumptions from the database.
        }

        // Obtaining a specific consumption by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsumption(int id)
        {
            var consumption = await _context.Consumptions.FindAsync(id); // Verify if the consumption exists.
            return consumption is not null ? Ok(consumption) : NotFound(); // Return the consumption if it exists, otherwise return a 404.
        }

        // Create a new consumption.
        [HttpPost]
        public async Task<IActionResult> CreateConsumption([FromBody] ConsumptionModel consumption)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            var stock = await _context.Stocks.FindAsync(consumption.StockID); // Verify if the stock exists.
            if (stock == null) return NotFound();
            _context.Entry(stock).State = EntityState.Unchanged; // Attach the existing Stock to the context to avoid creating a new one
            consumption.Stock = stock;

            _context.Consumptions.Add(consumption); // Add the new consumption to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetConsumption), new { id = consumption.ConsumptionID }, consumption);
        }

        // Update an existing consumption.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumption(int id, [FromBody] ConsumptionModel updatedConsumption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consumption = await _context.Consumptions.FindAsync(id); // Verify if the consumption exists.
            if (consumption == null) return NotFound();

            var stock = await _context.Stocks.FindAsync(updatedConsumption.StockID); // Verify if the stock exists.
            if (stock == null) return NotFound();
            _context.Entry(stock).State = EntityState.Unchanged; // Attach the existing Stock to the context to avoid creating a new one
            consumption.Stock = stock;

            consumption.Quantity = updatedConsumption.Quantity;
            consumption.ConsumptionDate = updatedConsumption.ConsumptionDate;
            consumption.StockID = updatedConsumption.StockID;

            await _context.SaveChangesAsync(); 
            return Ok(consumption);
        }

        // Delete a consumption.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumption(int id)
        {
            var consumption = await _context.Consumptions.FindAsync(id); // Verify if the consumption exists.
            if (consumption == null) return NotFound();

            _context.Consumptions.Remove(consumption); // Remove the consumption from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}