using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VoteRepository : RepositoryBase<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<Vote>> GetAllAsync()
        {
            return await _context.Votes
                .Include(v => v.Usuario)
                .Include(v => v.Libro)
                .ToListAsync();
        }

        public override async Task<Vote?> GetByIdAsync(int id)
        {
            return await _context.Votes
                .Include(v => v.Usuario)
                .Include(v => v.Libro)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vote>> GetByLibroIdAsync(int libroId)
        {
            return await _context.Votes
                .Where(v => v.LibroId == libroId)
                .Include(v => v.Usuario)
                .Include(v => v.Libro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vote>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Votes
                .Where(v => v.UsuarioId == usuarioId)
                .Include(v => v.Usuario)
                .Include(v => v.Libro)
                .ToListAsync();
        }
        public async Task<Vote?> GetByUsuarioYLibroAsync(int usuarioId, int libroId)
        {
            return await _context.Votes
                .Include(v => v.Usuario)
                .Include(v => v.Libro)
                .FirstOrDefaultAsync(v => v.UsuarioId == usuarioId && v.LibroId == libroId);
        }

    }
}
