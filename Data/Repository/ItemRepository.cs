using CRN_Technical_Assessment.Data.Entities;
using CRN_Technical_Assessment.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRN_Technical_Assessment.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Item>> GetByProductIdAsync(int productId)
        {
            return await _context.Items
                .AsNoTracking()
                .Where(i => i.ProductId == productId)
                .ToListAsync();
        }
         
        public async Task<Item> CreateAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Item?> UpdateAsync(Item item)
        {
            var existingItem = await _context.Items.FindAsync(item.Id);

            if (existingItem == null)
                return null;

            existingItem.ProductId = item.ProductId;
            existingItem.Quantity = item.Quantity;

            await _context.SaveChangesAsync();

            return existingItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return false;

            _context.Items.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}