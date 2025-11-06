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
            var lista = await _context.ReadingLists
                .Include(r => r.Libros)
                .FirstOrDefaultAsync(r => r.Id == listId);

            return lista?.Libros ?? new List<Book>();
        }

        public override async Task<IEnumerable<ReadingList>> GetAllAsync()
        {
            return await _context.ReadingLists
                .Include(r => r.Creador)    
                .Include(r => r.Libros)      
                .ToListAsync();
        }

        public async Task AddBookToListAsync(int listId, int bookId)
        {
            var lista = await _context.ReadingLists
                .Include(r => r.Libros)
                .FirstOrDefaultAsync(r => r.Id == listId);

            var book = await _context.Books.FindAsync(bookId);

            if (lista == null || book == null)
                throw new Exception("Lista o libro no encontrados.");

            if (!lista.Libros.Contains(book))
            {
                lista.Libros.Add(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RemoveBookFromListAsync(int listId, int bookId)
        {
            var lista = await _context.ReadingLists
                .Include(r => r.Libros)
                .FirstOrDefaultAsync(r => r.Id == listId);

            if (lista == null) return false;

            var book = lista.Libros.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return false;

            lista.Libros.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }


    }
}
