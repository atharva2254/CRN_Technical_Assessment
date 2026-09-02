using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using CRN_Technical_Assessment.Data.Entities;
using CRN_Technical_Assessment.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRN_Technical_Assessment.Application.Services
{
    public class AuthService(IUserRepository repo, IUserService service, IConfiguration configuration): IAuthService
    {
        public async Task<User?> RegisterUser(UserDto request)
        {
            return await service.CreateUser(request);
        }

        public async Task<string?> LoginUser(LoginDto request)
        {
            var user = await repo.GetByEmailAsync(request.Email);
            if(user is null)
            {
                return null;
            }

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateToken(user);
        }

        public bool LogoutAsync()
        {
            return true;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
                );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

            var tokenDecriptor = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                    audience: configuration.GetValue<string>("AppSettings:Audience"),
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(2),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDecriptor);
        }
    }
}
