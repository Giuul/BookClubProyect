using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingListsController : ControllerBase
    {
        private readonly IReadingListService _service;

        public ReadingListsController(IReadingListService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (role == "admin")
                return Ok(await _service.GetAllAsync());

            return Ok(await _service.GetAllVisibleForUserAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var list = await _service.GetByIdAsync(id);

            if (list == null) return NotFound("La lista no existe.");

            if (list.CreadorId != userId && !list.EsCompartida)
                return StatusCode(403, "No tienes permiso para ver esta lista.");

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReadingListDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            dto.CreadorId = userId;
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReadingListDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var list = await _service.GetByIdAsync(id);

            if (list == null) return NotFound("La lista no existe.");
            if (list.CreadorId != userId) return StatusCode(403, "No puedes modificar esta lista.");

            dto.CreadorId = userId;
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var list = await _service.GetByIdAsync(id);

            if (list == null) return NotFound("La lista no existe.");
            if (list.CreadorId != userId) return StatusCode(403, "No puedes eliminar esta lista.");

            await _service.DeleteAsync(id);
            return Ok("Lista eliminada con éxito.");
        }

        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetBooks(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var list = await _service.GetByIdAsync(id);

            if (list == null) return NotFound("La lista no existe.");
            if (list.CreadorId != userId && !list.EsCompartida)
                return StatusCode(403, "No tienes permiso para ver esta lista.");

            return Ok(await _service.GetBooksInListAsync(id));
        }

        [HttpPost("{listId}/books/{bookId}")]
        public async Task<IActionResult> AddBook(int listId, int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var list = await _service.GetByIdAsync(listId);

            if (list == null) return NotFound("La lista no existe.");
            if (list.CreadorId != userId) return StatusCode(403, "No puedes modificar esta lista.");

            var agregado = await _service.AddBookAsync(listId, bookId);

            if (!agregado)
                return Conflict("El libro ya está en esta lista.");

            return Ok("Libro agregado con éxito.");
        }

        [HttpDelete("{listId}/books/{bookId}")]
        public async Task<IActionResult> RemoveBook(int listId, int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var list = await _service.GetByIdAsync(listId);

            if (list == null) return NotFound("La lista no existe.");
            if (list.CreadorId != userId) return StatusCode(403, "No puedes modificar esta lista.");

            var removed = await _service.RemoveBookAsync(listId, bookId);
            if (!removed) return NotFound("El libro no está en esta lista.");

            return Ok("Libro eliminado.");
        }
    }
}
