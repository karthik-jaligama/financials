using Microsoft.EntityFrameworkCore;
using Financials.Data.Classes;

namespace Financials.Data.Services
{
    public class SavingsService
    {
        private readonly FinancialsContext _context;

        public SavingsService(FinancialsContext context)
        {
            _context = context;
        }

        public async Task<List<Saving>> GetSavingsAsync()
        {
            return await _context.Savings.ToListAsync();
        }

        public async Task<Saving?> GetSavingByIdAsync(int id)
        {
            return await _context.Savings.FindAsync(id);
        }

        public async Task<Saving> CreateSavingAsync(Saving saving)
        {
            _context.Savings.Add(saving);
            await _context.SaveChangesAsync();
            return saving;
        }

        public async Task<Saving> UpdateSavingAsync(Saving saving)
        {
            _context.Savings.Update(saving);
            await _context.SaveChangesAsync();
            return saving;
        }

        public async Task DeleteSavingAsync(int id)
        {
            var saving = await _context.Savings.FindAsync(id);
            if (saving != null)
            {
                _context.Savings.Remove(saving);
                await _context.SaveChangesAsync();
            }
        }
    }
}
