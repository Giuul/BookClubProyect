using Domain.Entities;

namespace Application.Models
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public int Valor { get; set; }

        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;

        public int LibroId { get; set; }
        public string LibroTitulo { get; set; } = string.Empty;

        public static VoteDTO Create(Vote vote)
            => new VoteDTO
            {
                Id = vote.Id,
                Valor = vote.Valor,
                UsuarioId = vote.UsuarioId,
                UsuarioNombre = vote.Usuario?.Nombre ?? string.Empty,
                LibroId = vote.LibroId,
                LibroTitulo = vote.Libro?.Titulo ?? string.Empty
            };

        public class VoteCreateDTO
        {
            public string LibroTitulo { get; set; } = string.Empty;
            public int Valor { get; set; }
        }

        public static List<VoteDTO> CreateList(IEnumerable<Vote> votes)
            => votes.Select(Create).ToList();
    }
}

