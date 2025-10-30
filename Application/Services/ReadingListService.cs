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
    }
}
