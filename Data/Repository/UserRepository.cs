using CRN_Technical_Assessment.Data.Entities;
using CRN_Technical_Assessment.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRN_Technical_Assessment.Data.Repository
{
    public class UserRepository(AppDbContext context): IUserRepository
    {
        public async Task<List<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        } 

        public async Task<User?> GetByEmailAsync(string Email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<User?> CreateAsync(User user)
        {
            if(await context.Users.AnyAsync(u=> u.Email == user.Email))
            {
                return null;
            }

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
