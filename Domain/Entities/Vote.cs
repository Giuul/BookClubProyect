using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Valor { get; set; }

        // Relaciones
        public int UsuarioId { get; set; }
        public User Usuario { get; set; } = null!;

        public int LibroId { get; set; }
        public Book Libro { get; set; } = null!;
    }
}