using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Data.Entities;

namespace CRN_Technical_Assessment.Application.Interfaces
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();

        Task<Item?> GetById(int id);

        Task<List<Item>> GetByProductsId(int productId);

        Task<Item?> CreateItem(ItemDto request);

        Task<Item?> UpdateItem(int id, ItemDto request);

        Task<bool> DeleteItem(int id);
    }
}