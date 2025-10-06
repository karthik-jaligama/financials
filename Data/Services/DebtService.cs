using Microsoft.EntityFrameworkCore;
using Financials.Data;

namespace Financials.Data.Services
{
    public class DebtService
    {
        private readonly FinancialsContext _context;

        public DebtService(FinancialsContext context)
        {
            _context = context;
        }

        public async Task<List<Debt>> GetDebtsAsync()
        {
            return await _context.Debts.ToListAsync();
        }

        public async Task<Debt?> GetDebtByIdAsync(int id)
        {
            return await _context.Debts.FindAsync(id);
        }

        public async Task<Debt> CreateDebtAsync(Debt debt)
        {
            _context.Debts.Add(debt);
            await _context.SaveChangesAsync();
            return debt;
        }

        public async Task<Debt> UpdateDebtAsync(Debt debt)
        {
            _context.Debts.Update(debt);
            await _context.SaveChangesAsync();
            return debt;
        }

        public async Task DeleteDebtAsync(int id)
        {
            var debt = await _context.Debts.FindAsync(id);
            if (debt != null)
            {
                _context.Debts.Remove(debt);
                await _context.SaveChangesAsync();
            }
        }
    }
}
