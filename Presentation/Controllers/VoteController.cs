using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Models.VoteDTO;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _service;

        public VotesController(IVoteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vote = await _service.GetByIdAsync(id);
            if (vote == null)
                return NotFound("No existe un voto con ese ID.");

            return Ok(vote);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoteCreateDTO dto)
        {
            try
            {
                var result = await _service.CreateAsync(dto, User);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound("No existe un voto con ese ID.");

            return Ok("Voto eliminado con éxito.");
        }
    }
}
