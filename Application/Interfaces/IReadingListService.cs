using Application.Models;

namespace Application.Interfaces
{
    public interface IReadingListService
    {
        Task<ICollection<ReadingListDTO>> GetAllAsync();
        Task<ReadingListDTO?> GetByIdAsync(int id);
        Task<ReadingListDTO> CreateAsync(ReadingListDTO dto);
        Task<ReadingListDTO> UpdateAsync(int id, ReadingListDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}