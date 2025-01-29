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
    public class StockReportController : ControllerBase
    {
        private readonly EFCoreContext _context; // The context is used to interact with the database.

        public StockReportController(EFCoreContext context) // The context is injected in the controller.
        {
            _context = context;
        }

        // Obtaining all stock reports.
        [HttpGet]
        public async Task<IActionResult> GetStockReports()
        {
            var stockReports = await _context.StockReports
                .Include(sr => sr.Stocks) // Include the Stocks collection
                .ToListAsync(); // Return all stock reports from the database.
            return Ok(stockReports);
        }

        // Obtaining a specific stockReport by ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockReport(int id)
        {
            var stockReport = await _context.StockReports
                .Include(sr => sr.Stocks) // Include the Stocks collection
                .FirstOrDefaultAsync(sr => sr.StockReportID == id); // Verify if the stockReport exists.
            return stockReport is not null ? Ok(stockReport) : NotFound(); // Return the stockReport if it exists, otherwise return a 404.
        }

        // Create a new stockReport.
        [HttpPost]
        public async Task<IActionResult> CreateStockReport([FromBody] StockReportModel stockReport)
        {
            if (!ModelState.IsValid) // Verify if the model is valid.
            {
                return BadRequest(ModelState);
            }

            _context.StockReports.Add(stockReport); // Add the new stockReport to the context.
            await _context.SaveChangesAsync(); // Save the changes to the database.
            return CreatedAtAction(nameof(GetStockReport), new { id = stockReport.StockReportID }, stockReport);
        }

        // Update an existing stockReport.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStockReport(int id, [FromBody] StockReportModel updatedStockReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockReport = await _context.StockReports.FindAsync(id); // Verify if the stockReport exists.
            if (stockReport == null) return NotFound();

            stockReport.Stocks = updatedStockReport.Stocks; // maybe better to just receive and insert new individual stock instead of the whole list

            await _context.SaveChangesAsync(); 
            return Ok(stockReport);
        }

        // Delete a stockReport.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockReport(int id)
        {
            var stockReport = await _context.StockReports.FindAsync(id); // Verify if the stockReport exists.
            if (stockReport == null) return NotFound();

            _context.StockReports.Remove(stockReport); // Remove the stockReport from the context.
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}