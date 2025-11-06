using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReadingListRepository : IRepositoryBase<ReadingList>
    {
        Task<IEnumerable<ReadingList>> GetByCreadorIdAsync(int creadorId);
        Task<IEnumerable<Book>> GetBooksByListIdAsync(int listId);
        Task AddBookToListAsync(int listId, int bookId);
        Task<bool> RemoveBookFromListAsync(int listId, int bookId);
        Task<Book?> GetBookByIdAsync(int bookId);
        Task DeleteBook(Book book);

    }
}

