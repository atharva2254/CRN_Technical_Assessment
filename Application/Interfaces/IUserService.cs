using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Data.Entities;

namespace CRN_Technical_Assessment.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User?> CreateUser(UserDto request);
        Task<bool> UpdateUser(User request);
        Task<bool> DeleteUser(int id);
    }
}
