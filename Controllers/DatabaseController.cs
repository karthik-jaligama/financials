using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Financials.Data;
using Financials.Data.Services;

namespace Financials.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseInitializationService _dbInitService;
        private readonly FinancialsContext _context;

        public DatabaseController(DatabaseInitializationService dbInitService, FinancialsContext context)
        {
            _dbInitService = dbInitService;
            _context = context;
        }

        [HttpPost("initialize-database")]
        public async Task<IActionResult> InitializeDatabase()
        {
            try
            {
                await _dbInitService.InitializeDatabaseAsync();
                return Ok(new { message = "Database initialized successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to initialize database", error = ex.Message });
            }
        }

        [HttpPost("reset-database")]
        public async Task<IActionResult> ResetDatabase()
        {
            try
            {
                // Delete all data from all tables
                _context.Assets.RemoveRange(_context.Assets);
                _context.Debts.RemoveRange(_context.Debts);
                _context.Investments.RemoveRange(_context.Investments);
                _context.Savings.RemoveRange(_context.Savings);
                
                await _context.SaveChangesAsync();
                
                // Re-initialize with seed data
                await _dbInitService.InitializeDatabaseAsync();
                
                return Ok(new { message = "Database reset successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to reset database", error = ex.Message });
            }
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetDatabaseStatus()
        {
            try
            {
                var assetCount = await _context.Assets.CountAsync();
                var debtCount = await _context.Debts.CountAsync();
                var investmentCount = await _context.Investments.CountAsync();
                var savingsCount = await _context.Savings.CountAsync();

                return Ok(new 
                { 
                    isInitialized = assetCount > 0,
                    assetCount,
                    debtCount,
                    investmentCount,
                    savingsCount
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to get database status", error = ex.Message });
            }
        }
    }
}
