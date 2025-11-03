using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookSearchController : ControllerBase
    {
        private readonly IThirdPartyApiClient _apiClient;

        public BookSearchController(IThirdPartyApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("El parámetro 'query' es obligatorio para la búsqueda.");
            }

            try
            {
                var jsonResponse = await _apiClient.SearchBooksAsync(query);

                return Content(jsonResponse, "application/json");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, $"Error al contactar el servicio de libros: {ex.Message}");
            }
        }
    }
}