using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetByGeneroAsync(string genero)
        {
            return await _context.Books
                .Where(b => b.Genero.ToLower() == genero.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Vote>> GetVotesByBookIdAsync(int bookId)
        {
            return await _context.Votes
                .Where(v => v.LibroId == bookId)
                .Include(v => v.Usuario)
                .ToListAsync();
        }

        public override async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.ListaLectura)
                .Include(b => b.Votos)         
                .FirstOrDefaultAsync(b => b.Id == id);
        }

    }
}
