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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (userRole != "admin")
                return StatusCode(403, new { message = "No tienes permisos para ver los usuarios." });

            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var currentUserRole = User.FindFirst(ClaimTypes.Role)!.Value;

            if (currentUserRole != "admin" && currentUserId != id)
                return StatusCode(403, new { message = "No tienes permisos para ver la información de este usuario." });

            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound(new { message = "El usuario no existe." });

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDTO dto)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var currentUserRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)!.Value;

            if (currentUserRole != "admin" && currentUserId != id)
                return StatusCode(403, new { message = "No tienes permisos para editar este usuario." });

            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (Exception e)
            {
                return NotFound(new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

            if (currentUserId != id && userRole != "admin")
                return StatusCode(403, "No tienes permisos para eliminar este usuario.");

            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound("El usuario no existe.");

            return NoContent();
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMe()
        {
            var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var result = await _service.DeleteAsync(currentUserId);

            if (!result)
                return NotFound(new { message = "El usuario no existe o ya fue eliminado." });

            return Ok(new { message = "Tu cuenta ha sido eliminada correctamente." });
        }

    }
}
