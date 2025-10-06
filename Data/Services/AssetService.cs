using Microsoft.EntityFrameworkCore;
using Financials.Data;

namespace Financials.Data.Services
{
    public class AssetService
    {
        private readonly FinancialsContext _context;

        public AssetService(FinancialsContext context)
        {
            _context = context;
        }

        public async Task<List<Asset>> GetAssetsAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task DeleteAssetAsync(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
            }
        }
    }
}
