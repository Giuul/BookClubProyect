using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReadingList
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsCompartida { get; set; } = false;

        // Relaciones
        public int CreadorId { get; set; }
        public User Creador { get; set; } = null!;

        public List<Book> Libros { get; set; } = new List<Book>();
    }
}