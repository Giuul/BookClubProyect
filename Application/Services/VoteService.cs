using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System.Security.Claims;
using static Application.Models.VoteDTO;

namespace Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepo;
        private readonly IBookRepository _bookRepo;

        public VoteService(IVoteRepository voteRepo, IBookRepository bookRepo)
        {
            _voteRepo = voteRepo;
            _bookRepo = bookRepo;
        }

        public async Task<ICollection<VoteDTO>> GetAllAsync()
            => VoteDTO.CreateList(await _voteRepo.GetAllAsync());

        public async Task<VoteDTO?> GetByIdAsync(int id)
        {
            var vote = await _voteRepo.GetByIdAsync(id);
            return vote == null ? null : VoteDTO.Create(vote);
        }

        public async Task<VoteDTO> CreateAsync(VoteCreateDTO dto, ClaimsPrincipal user)
        {
            var usuarioId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var libro = await _bookRepo.GetByTituloAsync(dto.LibroTitulo);
            if (libro == null)
                throw new Exception("No existe un libro con ese título.");

            var votoExistente = await _voteRepo.GetByUsuarioYLibroAsync(usuarioId, libro.Id);

            if (votoExistente != null)
            {
                votoExistente.Valor = dto.Valor;
                _voteRepo.Update(votoExistente);
                await _voteRepo.SaveChangesAsync();
                return VoteDTO.Create(votoExistente);
            }

            var vote = new Vote
            {
                Valor = dto.Valor,
                LibroId = libro.Id,
                UsuarioId = usuarioId
            };

            await _voteRepo.AddAsync(vote);
            await _voteRepo.SaveChangesAsync();
            return VoteDTO.Create(vote);
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var vote = await _voteRepo.GetByIdAsync(id);
            if (vote == null) return false;

            _voteRepo.Delete(vote);
            await _voteRepo.SaveChangesAsync();
            return true;
        }
    }
}
