using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<ReadingList>> GetListsByUserIdAsync(int userId);
    }
}
