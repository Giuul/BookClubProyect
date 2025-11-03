using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using BCrypt.Net;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<ICollection<UserDTO>> GetAllAsync()
            => UserDTO.CreateList(await _repo.GetAllAsync());

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user == null ? null : UserDTO.Create(user);
        }

        public async Task<UserDTO> RegisterAsync(UserDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Password))
                throw new ArgumentException("La contraseña no puede estar vacía.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = hashedPassword,
                Rol = Enum.TryParse<Rol>(dto.Rol, out var rol) ? rol : Rol.usuario
            };

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();
            return UserDTO.Create(user);
        }

        public async Task<UserDTO> UpdateAsync(int id, UserDTO dto)
        {
            var user = await _repo.GetByIdAsync(id) ?? throw new Exception("Usuario no encontrado.");

            user.Nombre = dto.Nombre;
            user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            if (!string.IsNullOrEmpty(dto.Rol) && Enum.TryParse<Rol>(dto.Rol, out var rol))
                user.Rol = rol;

            _repo.Update(user);
            await _repo.SaveChangesAsync();
            return UserDTO.Create(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            _repo.Delete(user);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
