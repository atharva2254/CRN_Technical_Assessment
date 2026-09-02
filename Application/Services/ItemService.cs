using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using CRN_Technical_Assessment.Data.Entities;
using CRN_Technical_Assessment.Data.Repositories;
using CRN_Technical_Assessment.Data.Repositories.Interfaces;
using CRN_Technical_Assessment.Data.Repository.Interfaces;

namespace CRN_Technical_Assessment.Application.Services
{
    public class ItemService(IItemRepository _itemRepository, IProductRepository _productRepository) : IItemService
    {
        public async Task<List<Item>> GetAllItems()
        {
            return await _itemRepository.GetAllAsync();
        }

        public async Task<Item?> GetById(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);

            return item == null ? null : item;
        }

        public async Task<List<Item>> GetByProductsId(int productId)
        {
            return await _itemRepository.GetByProductIdAsync(productId);

        }

        public async Task<Item?> CreateItem(ItemDto request)
        {
            var product = await _productRepository
                .GetByIdAsync(request.ProductId);

            if (product == null)
                return null;

            var item = new Item
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            return await _itemRepository.CreateAsync(item);

        }

        public async Task<Item?> UpdateItem(int id, ItemDto request)
        {
            var existingItem = await _itemRepository
                .GetByIdAsync(id);

            if (existingItem == null)
                return null;

            var product = await _productRepository
                .GetByIdAsync(request.ProductId);

            if (product == null)
                return null;

            var item = new Item
            {
                Id = id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            return await _itemRepository.UpdateAsync(item);

        }

        public async Task<bool> DeleteItem(int id)
        {
            return await _itemRepository.DeleteAsync(id);
        }
    }
}