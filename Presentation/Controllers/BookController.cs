using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System; 

namespace Presentation.Controllers
{
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDTO dto)
        {
            try
            {
                var book = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDTO dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Libro no encontrado"))
                {
                    return NotFound(e.Message);
                }

                return StatusCode(500, "Ocurrió un error inesperado al actualizar el libro.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound("No se pudo eliminar el libro. El ID no existe.");

            return Ok("Libro eliminado con éxito.");
        }

        [HttpDelete("{bookId}/lists/{readingListId}")]
        public async Task<IActionResult> RemoveFromReadingList(int bookId, int readingListId)
        {
            try
            {
                var success = await _service.RemoveBookFromReadingListAsync(bookId, readingListId);

                if (success)
                {
                    return NoContent();
                }

                return NotFound("No se encontró el libro o la lista, o el libro ya no estaba en la lista.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno al remover el libro: {ex.Message}");
            }
        }
    }
}