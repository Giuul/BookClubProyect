using Application.Models;
using System.Security.Claims;
using static Application.Models.VoteDTO;

public interface IVoteService
{
    Task<ICollection<VoteDTO>> GetAllAsync();
    Task<VoteDTO?> GetByIdAsync(int id);
    Task<VoteDTO> CreateAsync(VoteCreateDTO dto, ClaimsPrincipal user);
    Task<bool> DeleteAsync(int id);
}

