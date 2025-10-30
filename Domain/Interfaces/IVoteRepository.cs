using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVoteRepository : IRepositoryBase<Vote>
    {
        Task<IEnumerable<Vote>> GetByLibroIdAsync(int libroId);
        Task<IEnumerable<Vote>> GetByUsuarioIdAsync(int usuarioId);
    }
}
