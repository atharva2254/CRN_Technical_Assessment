using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Data.Entities;

namespace CRN_Technical_Assessment.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterUser(UserDto request);
        Task<string?> LoginUser(LoginDto request);
        bool LogoutAsync();
    }
}