using Microsoft.EntityFrameworkCore;
using Financials.Data;

namespace Financials.Data.Services
{
    public class InvestmentService
    {
        private readonly FinancialsContext _context;

        public InvestmentService(FinancialsContext context)
        {
            _context = context;
        }

        public async Task<List<Investment>> GetInvestmentsAsync()
        {
            return await _context.Investments.ToListAsync();
        }

        public async Task<Investment?> GetInvestmentByIdAsync(int id)
        {
            return await _context.Investments.FindAsync(id);
        }

        public async Task<Investment> CreateInvestmentAsync(Investment investment)
        {
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();
            return investment;
        }

        public async Task<Investment> UpdateInvestmentAsync(Investment investment)
        {
            _context.Investments.Update(investment);
            await _context.SaveChangesAsync();
            return investment;
        }

        public async Task DeleteInvestmentAsync(int id)
        {
            var investment = await _context.Investments.FindAsync(id);
            if (investment != null)
            {
                _context.Investments.Remove(investment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
