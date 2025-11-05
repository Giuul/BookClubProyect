using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReadingListService : IReadingListService
    {
        private readonly IReadingListRepository _repo;

        public ReadingListService(IReadingListRepository repo) => _repo = repo;

        public async Task<ICollection<ReadingListDTO>> GetAllAsync()
            => ReadingListDTO.CreateList(await _repo.GetAllAsync());

        public async Task<ReadingListDTO?> GetByIdAsync(int id)
        {
            var list = await _repo.GetByIdAsync(id);
            return list == null ? null : ReadingListDTO.Create(list);
        }

        public async Task<ReadingListDTO> CreateAsync(ReadingListDTO dto)
        {
            var list = new ReadingList
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                EsCompartida = dto.EsCompartida,
                CreadorId = dto.CreadorId
            };

            await _repo.AddAsync(list);
            await _repo.SaveChangesAsync();
            return ReadingListDTO.Create(list);
        }

        public async Task<ReadingListDTO> UpdateAsync(int id, ReadingListDTO dto)
        {
            var list = await _repo.GetByIdAsync(id) ?? throw new Exception("Lista no encontrada.");

            list.Titulo = dto.Titulo;
            list.Descripcion = dto.Descripcion;
            list.EsCompartida = dto.EsCompartida;
            list.CreadorId = dto.CreadorId;

            _repo.Update(list);
            await _repo.SaveChangesAsync();
            return ReadingListDTO.Create(list);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var list = await _repo.GetByIdAsync(id);
            if (list == null) return false;

            _repo.Delete(list);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<ReadingListDTO>> GetAllVisibleForUserAsync(int userId)
        {
            var all = await _repo.GetAllAsync();

            var visible = all.Where(r =>
                r.CreadorId == userId ||   
                r.EsCompartida             
            );

            return ReadingListDTO.CreateList(visible);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksInListAsync(int listId)
        {
            var books = await _repo.GetBooksByListIdAsync(listId);
            return books.Select(b => BookDTO.Create(b));
        }

        public async Task AddBookAsync(int listId, int bookId)
        {
            await _repo.AddBookToListAsync(listId, bookId);
            await _repo.SaveChangesAsync();
        }

        public async Task<bool> RemoveBookAsync(int listId, int bookId)
        {
            var list = await _repo.GetByIdAsync(listId);
            if (list == null) return false;

            var book = list.Libros.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return false;

            _repo.DeleteBook(book);
            await _repo.SaveChangesAsync();

            return true;
        }
    }
}
