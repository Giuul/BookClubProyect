using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System; 

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;

        public BookService(IBookRepository repo) => _repo = repo;

        public async Task<ICollection<BookDTO>> GetAllAsync()
            => BookDTO.CreateList(await _repo.GetAllAsync());

        public async Task<BookDTO?> GetByIdAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            return book == null ? null : BookDTO.Create(book);
        }

        public async Task<BookDTO> CreateAsync(BookDTO dto)
        {
            var existingBook = await _repo.GetByTitleAndAuthorAsync(dto.Titulo, dto.Autor);

            if (existingBook != null)
            {
                throw new InvalidOperationException("Ya existe un libro con el mismo título y autor.");
            }

            var book = new Book
            {
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                Genero = dto.Genero,
                Resenia = dto.Resenia
            };

            await _repo.AddAsync(book);
            await _repo.SaveChangesAsync();
            return BookDTO.Create(book);
        }

        public async Task<BookDTO> UpdateAsync(int id, BookDTO dto)
        {
            var book = await _repo.GetByIdAsync(id) ?? throw new Exception("Libro no encontrado.");

            if (book.Titulo != dto.Titulo || book.Autor != dto.Autor)
            {
                var existingBook = await _repo.GetByTitleAndAuthorAsync(dto.Titulo, dto.Autor);

                if (existingBook != null && existingBook.Id != id) 
                {
                    throw new InvalidOperationException("La edición intenta crear un libro duplicado (mismo título y autor de otro libro).");
                }
            }

            book.Titulo = dto.Titulo;
            book.Autor = dto.Autor;
            book.Genero = dto.Genero;
            book.Resenia = dto.Resenia;

            _repo.Update(book);
            await _repo.SaveChangesAsync();
            return BookDTO.Create(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) return false;

            _repo.Delete(book);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}