using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<ReadingList>> GetListsByUserIdAsync(int userId)
        {
            return await _context.ReadingLists
                .Where(r => r.CreadorId == userId)
                .Include(r => r.Libros)
                .ToListAsync();
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.ListasCreadas)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

    }
}