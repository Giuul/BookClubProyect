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

        public override async Task<IEnumerable<ReadingList>> GetAllAsync()
        {
            return await _context.ReadingLists
                .Include(r => r.Creador)    
                .Include(r => r.Libros)      
                .ToListAsync();
        }

        public async Task AddBookToListAsync(int listId, int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null) throw new Exception("El libro no existe.");

            book.ListId = listId;
            _context.Books.Update(book);
        }

        public async Task<bool> RemoveBookFromListAsync(int listId, int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId && b.ListId == listId);
            if (book == null) return false;
            book.ListId = 0;
            _context.Books.Update(book);
            return true;
        }
        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        }
        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }

    }
}
