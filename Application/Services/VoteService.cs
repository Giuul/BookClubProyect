using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _repo;

        public VoteService(IVoteRepository repo) => _repo = repo;

        public async Task<ICollection<VoteDTO>> GetAllAsync()
            => VoteDTO.CreateList(await _repo.GetAllAsync());

        public async Task<VoteDTO?> GetByIdAsync(int id)
        {
            var vote = await _repo.GetByIdAsync(id);
            return vote == null ? null : VoteDTO.Create(vote);
        }

        public async Task<VoteDTO> CreateAsync(VoteDTO dto)
        {
            var vote = new Vote
            {
                Valor = dto.Valor,
                UsuarioId = dto.UsuarioId,
                LibroId = dto.LibroId
            };

            await _repo.AddAsync(vote);
            await _repo.SaveChangesAsync();
            return VoteDTO.Create(vote);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vote = await _repo.GetByIdAsync(id);
            if (vote == null) return false;

            _repo.Delete(vote);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
