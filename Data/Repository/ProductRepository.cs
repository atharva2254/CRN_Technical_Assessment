using CRN_Technical_Assessment.Data.Entities;
using CRN_Technical_Assessment.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRN_Technical_Assessment.Data.Repository
{
    public class ProductRepository(AppDbContext context): IProductRepository
    {
        public async Task<List<Product>> GetAllAsync()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct = await context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct == null)
                return null;

            existingProduct.ProductName = product.ProductName;
            existingProduct.ModifiedBy = product.ModifiedBy;
            existingProduct.ModifiedOn = product.ModifiedOn;

            await context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return false;

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
