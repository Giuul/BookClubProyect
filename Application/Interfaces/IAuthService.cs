using Application.Models;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginRequestDTO dto);
        Task<UserDTO> RegisterAsync(LoginRequestDTO dto);
    }
}
