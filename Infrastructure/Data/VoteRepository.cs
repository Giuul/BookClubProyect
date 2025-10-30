using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VoteRepository : RepositoryBase<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Vote>> GetByLibroIdAsync(int libroId)
        {
            return await _context.Votes
                .Where(v => v.LibroId == libroId)
                .Include(v => v.Usuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vote>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Votes
                .Where(v => v.UsuarioId == usuarioId)
                .Include(v => v.Libro)
                .ToListAsync();
        }
    }
}
