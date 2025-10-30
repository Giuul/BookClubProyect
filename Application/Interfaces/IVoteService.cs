using Application.Models;

namespace Application.Interfaces
{
    public interface IVoteService
    {
        Task<ICollection<VoteDTO>> GetAllAsync();
        Task<VoteDTO?> GetByIdAsync(int id);
        Task<VoteDTO> CreateAsync(VoteDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}

