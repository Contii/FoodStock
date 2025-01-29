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
    public class SpoilReportController : ControllerBase
    {
        private readonly EFCoreContext _context; // The context is used to interact with the database.

        public SpoilReportController(EFCoreContext context) // The context is injected in the controller.
        {
            _context = context;
        }

        // Obtaining all spoil reports.
        [HttpGet]
        public async Task<IActionResult> GetSpoilReports()
        {
            var spoilReports = await _context.SpoilReports
                .Include(sr => sr.Itens) // Include the Itens collection
                .ToListAsync(); // Return all spoil reports from the database.
            return Ok(spoilReports);
        }

        // Obtaining a specific spoilReport by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpoilReport(int id)
        {
            var spoilReport = await _context.SpoilReports
                .Include(sr => sr.Itens) // Include the Itens collection
                .FirstOrDefaultAsync(sr => sr.SpoilReportID == id); // Verify if the spoilReport exists.
            return spoilReport is not null ? Ok(spoilReport) : NotFound(); // Return the spoilReport if it exists, otherwise return a 404.
        }


        // Create a new spoilReport.
        [HttpPost]
        public async Task<IActionResult> CreateSpoilReport([FromBody] SpoilReportModel spoilReport)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            _context.SpoilReports.Add(spoilReport); // Add the new spoilReport to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetSpoilReport), new { id = spoilReport.SpoilReportID }, spoilReport);
        }

        // Update an existing spoilReport.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpoilReport(int id, [FromBody] SpoilReportModel updatedSpoilReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spoilReport = await _context.SpoilReports.FindAsync(id); // Verify if the spoilReport exists.
            if (spoilReport == null) return NotFound();

            spoilReport.Itens = updatedSpoilReport.Itens; // maybe better to just receive and insert new individual spoil instead of the whole list

            await _context.SaveChangesAsync(); 
            return Ok(spoilReport);
        }

        // Delete a spoilReport.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpoilReport(int id)
        {
            var spoilReport = await _context.SpoilReports.FindAsync(id); // Verify if the spoilReport exists.
            if (spoilReport == null) return NotFound();

            _context.SpoilReports.Remove(spoilReport); // Remove the spoilReport from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}