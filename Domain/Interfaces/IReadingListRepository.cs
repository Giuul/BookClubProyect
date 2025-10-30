using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReadingListRepository : IRepositoryBase<ReadingList>
    {
        Task<IEnumerable<ReadingList>> GetByCreadorIdAsync(int creadorId);
        Task<IEnumerable<Book>> GetBooksByListIdAsync(int listId);
    }
}

