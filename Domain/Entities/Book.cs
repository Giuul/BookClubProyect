using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string? Resenia { get; set; }

        // Relaciones
        public ICollection<ReadingList> ReadingLists { get; set; } = new List<ReadingList>();
        public List<Vote> Votos { get; set; } = new List<Vote>();
    }
}