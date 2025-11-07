using Application.Models;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<ICollection<BookDTO>> GetAllAsync();
        Task<BookDTO?> GetByIdAsync(int id);
        Task<BookDTO> CreateAsync(BookDTO dto);
        Task<BookDTO> UpdateAsync(int id, BookDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}

