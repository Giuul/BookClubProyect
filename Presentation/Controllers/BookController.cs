using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDTO dto)
        {
            var role = User.FindFirst("role")?.Value;

            if (role != "Admin")
                return StatusCode(403, "No tienes permisos para crear libros.");

            var book = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDTO dto)
        {
            var role = User.FindFirst("role")?.Value;

            if (role != "Admin")
                return StatusCode(403, "No tienes permisos para actualizar libros.");

            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = User.FindFirst("role")?.Value;

            if (role != "Admin")
                return StatusCode(403, "No tienes permisos para eliminar este libro.");

            try
            {
                var result = await _service.DeleteAsync(id);

                if (!result)
                    return NotFound("No se pudo eliminar el libro. El ID no existe.");

                return Ok("Libro eliminado con éxito.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al eliminar el libro.");
            }
        }

        [Authorize]
        [HttpDelete("{bookId}/lists/{readingListId}")]
        public async Task<IActionResult> RemoveFromReadingList(int bookId, int readingListId)
        {
            try
            {
                var success = await _service.RemoveBookFromReadingListAsync(bookId, readingListId);

                if (success)
                    return NoContent();

                return NotFound("No se encontró el libro o la lista, o el libro ya no estaba en la lista.");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "No tienes permisos para eliminar este libro de la lista.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al remover el libro: {ex.Message}");
            }
        }
    }
}
