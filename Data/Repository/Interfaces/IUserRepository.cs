using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Data.Entities;

namespace CRN_Technical_Assessment.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string Email);
        Task<User?> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
