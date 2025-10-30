using Application.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int id);
        Task<UserDTO> RegisterAsync(UserDTO dto);
        Task<UserDTO> UpdateAsync(int id, UserDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
