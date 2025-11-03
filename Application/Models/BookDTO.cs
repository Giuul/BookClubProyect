using Domain.Entities;

namespace Application.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string? Resenia { get; set; }

        public int ListId { get; set; }
        public string ListaLecturaTitulo { get; set; } = string.Empty;

        public static BookDTO Create(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Titulo = book.Titulo,
                Autor = book.Autor,
                Genero = book.Genero,
                Resenia = book.Resenia,
                ListId = book.ListId,
                ListaLecturaTitulo = book.ListaLectura?.Titulo ?? "Sin lista"
            };
        }

        public static List<BookDTO> CreateList(IEnumerable<Book> books)
            => books.Select(Create).ToList();
    }
}
