using CRN_Technical_Assessment.Data.Entities;

namespace CRN_Technical_Assessment.Data.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync();

        Task<Item?> GetByIdAsync(int id);

        Task<List<Item>> GetByProductIdAsync(int productId);

        Task<Item> CreateAsync(Item item);

        Task<Item?> UpdateAsync(Item item);

        Task<bool> DeleteAsync(int id);
    }
}