using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Data.Entities;

namespace CRN_Technical_Assessment.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();

        Task<Product?> GetById(int id);
        Task<PagedResponse<Product>> GetAllAsync(int pageNumber, int pageSize);

        Task<Product> CreateProduct(ProductDto request, string username);

        Task<Product?> UpdateProduct(int id, ProductDto request, string username);

        Task<bool> DeleteProduct(int id);
    }
}