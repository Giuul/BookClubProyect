using Domain.Entities;

namespace Application.Models
{
    public class ReadingListDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsCompartida { get; set; }
        public int CreadorId { get; set; }
        public string CreadorNombre { get; set; } = string.Empty;

        public static ReadingListDTO Create(ReadingList list)
            => new ReadingListDTO
            {
                Id = list.Id,
                Titulo = list.Titulo,
                Descripcion = list.Descripcion,
                EsCompartida = list.EsCompartida,
                CreadorId = list.CreadorId,
                CreadorNombre = list.Creador.Nombre
            };

        public static List<ReadingListDTO> CreateList(IEnumerable<ReadingList> lists)
            => lists.Select(Create).ToList();
    }
}
