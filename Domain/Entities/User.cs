using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum Rol
    {
        usuario,
        admin,
    }
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Rol Rol { get; set; }

        // Relaciones
        public List<ReadingList> ListasCreadas { get; set; } = new List<ReadingList>();
        public List<Vote> Votos { get; set; } = new List<Vote>();
    }
}