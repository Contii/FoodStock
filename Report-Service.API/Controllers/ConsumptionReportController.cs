using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Report_Service.API.Controllers
{
    [ApiController] // Activates the class in an API controller design.
    [Route("api/[controller]")] // The route will be the controller name, supressing "Controller" from it.
    [EnableCors("AllowAll")] // Enable CORS for the controller.
    public class ConsumptionReportController : ControllerBase
    {
        private readonly EFCoreContext _context; // The context is used to interact with the database.

        public ConsumptionReportController(EFCoreContext context) // The context is injected in the controller.
        {
            _context = context;
        }

        // Obtaining all consumption reports.
        [HttpGet]
        public async Task<IActionResult> GetConsumptionReports()
        {
            var consumptionReports = await _context.ConsumptionReports
                .Include(cr => cr.Consumptions) // Include the Consumptions collection
                .ToListAsync(); // Return all consumption reports from the database.
            return Ok(consumptionReports);
        }

        // Obtaining a specific consumptionReport by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsumptionReport(int id)
        {
            var consumptionReport = await _context.ConsumptionReports
                .Include(cr => cr.Consumptions) // Include the Consumptions collection
                .FirstOrDefaultAsync(cr => cr.ConsumptionReportID == id); // Verify if the consumptionReport exists.
            return consumptionReport is not null ? Ok(consumptionReport) : NotFound(); // Return the consumptionReport if it exists, otherwise return a 404.
        }

        // Create a new consumptionReport.
        [HttpPost]
        public async Task<IActionResult> CreateConsumptionReport([FromBody] ConsumptionReportModel consumptionReport)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            _context.ConsumptionReports.Add(consumptionReport); // Add the new consumptionReport to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetConsumptionReport), new { id = consumptionReport.ConsumptionReportID }, consumptionReport);
        }

        // Update an existing consumptionReport.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumptionReport(int id, [FromBody] ConsumptionReportModel updatedConsumptionReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consumptionReport = await _context.ConsumptionReports.FindAsync(id); // Verify if the consumptionReport exists.
            if (consumptionReport == null) return NotFound();

            consumptionReport.Consumptions = updatedConsumptionReport.Consumptions; // maybe better to just receive and insert new individual stock instead of the whole list

            await _context.SaveChangesAsync(); 
            return Ok(consumptionReport);
        }

        // Delete a consumptionReport.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumptionReport(int id)
        {
            var consumptionReport = await _context.ConsumptionReports.FindAsync(id); // Verify if the consumptionReport exists.
            if (consumptionReport == null) return NotFound();

            _context.ConsumptionReports.Remove(consumptionReport); // Remove the consumptionReport from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}