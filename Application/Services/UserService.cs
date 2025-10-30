using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

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
            var user = new User
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password ?? string.Empty,
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
                user.Password = dto.Password;
            user.Rol = Enum.TryParse<Rol>(dto.Rol, out var rol) ? rol : Rol.usuario;

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
