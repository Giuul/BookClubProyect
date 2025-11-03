using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginRequestDTO dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null) return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!validPassword) return null;

            var token = GenerateJwtToken(user);
            return new AuthResponseDTO
            {
                Token = token,
                User = UserDTO.Create(user)
            };
        }

        public async Task<UserDTO> RegisterAsync(LoginRequestDTO dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Nombre = dto.Email.Split('@')[0], 
                Email = dto.Email,
                Password = hashedPassword,
                Rol = Rol.usuario
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return UserDTO.Create(user);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol.ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],     
                audience: _config["Jwt:Audience"],  
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
