using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using CRN_Technical_Assessment.Data.Entities;
using CRN_Technical_Assessment.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CRN_Technical_Assessment.Application.Services
{
    public class UserService(IUserRepository _repo): IUserService
    {
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _repo.GetAllAsync();

            return users;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            if(user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> CreateUser(UserDto request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
            };
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;

            return await _repo.CreateAsync(user);
        }

        public async Task<bool> UpdateUser(User request)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            if(user is null)
            {
                return false;
            }

            user.Name = request.Name;
            user.Email = request.Email;

            await _repo.UpdateAsync(user);
            return true;

        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _repo.GetByIdAsync(id);

            if(user is null)
            {
                return false;
            }

            await _repo.DeleteAsync(user);
            return true;
        }
    }
}
