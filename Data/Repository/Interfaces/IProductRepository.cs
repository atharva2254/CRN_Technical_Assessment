using CRN_Technical_Assessment.Data.Entities;

namespace CRN_Technical_Assessment.Data.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> CreateAsync(Product product);
        
        Task<Product?> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);
    }
}
