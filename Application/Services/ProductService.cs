using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using CRN_Technical_Assessment.Data.Entities;
using CRN_Technical_Assessment.Data.Repository;
using CRN_Technical_Assessment.Data.Repository.Interfaces;

namespace CRN_Technical_Assessment.Application.Services
{
    public class ProductService(IProductRepository productRepository): IProductService
    {
        public async Task<List<Product>> GetAllProducts()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if(product is null)
            {
                return null;
            }

            return product;
        }

        public async Task<PagedResponse<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
            var (products, totalRecords) = await productRepository.GetRequiredAsync(pageNumber, pageSize);

            var productDtos = products
                .Select(p => new Product
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = p.ModifiedOn
                })
                .ToList();

            return new PagedResponse<Product>
            {
                Items = productDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(
                    totalRecords / (double)pageSize)
            };
        }

        public async Task<Product> CreateProduct(ProductDto request, string username)
        { 

            var product = new Product
            {
                ProductName = request.ProductName,
                CreatedBy = username,
                CreatedOn = DateTime.UtcNow
            };

            var createdProduct = await productRepository.CreateAsync(product);

            return createdProduct;
        }

        public async Task<Product?> UpdateProduct(int id, ProductDto request, string username)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
                return null;


            var updatedProduct = new Product
            {
                Id = id,
                ProductName = request.ProductName,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn,
                ModifiedBy = username,
                ModifiedOn = DateTime.UtcNow
            };

            var result = await productRepository.UpdateAsync(updatedProduct);

            return result == null
                ? null
                : result;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await productRepository.DeleteAsync(id);
        }
    }
}
