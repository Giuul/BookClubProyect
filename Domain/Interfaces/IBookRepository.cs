using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<IEnumerable<Book>> GetByGeneroAsync(string genero);
        Task<IEnumerable<Vote>> GetVotesByBookIdAsync(int bookId);
        Task<Book?> GetByTituloAsync(string titulo);
        Task<Book?> GetByTitleAndAuthorAsync(string title, string author);
    }
}


