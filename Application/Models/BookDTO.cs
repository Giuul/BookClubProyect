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

        public List<ReadingListInfoDTO> ListasLectura { get; set; } = new();

        public static BookDTO Create(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Titulo = book.Titulo,
                Autor = book.Autor,
                Genero = book.Genero,
                Resenia = book.Resenia,
                ListasLectura = book.ReadingLists?
                    .Select(rl => new ReadingListInfoDTO
                    {
                        Id = rl.Id,
                        Titulo = rl.Titulo
                    })
                    .ToList() ?? new List<ReadingListInfoDTO>()
            
            };
        }

        public static List<BookDTO> CreateList(IEnumerable<Book> books)
            => books.Select(Create).ToList();

        public class ReadingListInfoDTO
        {
            public int Id { get; set; }
            public string Titulo { get; set; } = string.Empty;
        }
    }
}
