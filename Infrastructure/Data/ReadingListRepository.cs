using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReadingListRepository : RepositoryBase<ReadingList>, IReadingListRepository
    {
        public ReadingListRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ReadingList>> GetByCreadorIdAsync(int creadorId)
        {
            return await _context.ReadingLists
                .Where(r => r.CreadorId == creadorId)
                .Include(r => r.Libros)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByListIdAsync(int listId)
        {
            return await _context.Books
                .Where(b => b.ListId == listId)
                .ToListAsync();
        }
    }
}
