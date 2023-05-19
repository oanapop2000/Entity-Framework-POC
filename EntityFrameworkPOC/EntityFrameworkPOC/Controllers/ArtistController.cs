using EntityFrameworkPOC.Models;
using EntityFrameworkPOC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkPOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistLibraryService _libraryService;

        public ArtistController(IArtistLibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await _libraryService.GetArtistsAsync();

            if (artists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No artists in database");
            }

            return StatusCode(StatusCodes.Status200OK, artists);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetArtist(Guid id, bool includeSongs = false)
        {
            Artist artist = await _libraryService.GetArtistAsync(id, includeSongs);

            if (artist == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Artist found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, artist);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> AddArtist(Artist artist)
        {
            var dbArtist = await _libraryService.AddArtistAsync(artist);

            if (dbArtist == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{artist.Name} could not be added.");
            }

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateArtist(Guid id, Artist artist)
        {
            if (id != artist.Id)
            {
                return BadRequest();
            }

            Artist dbArtist = await _libraryService.UpdateArtistAsync(artist);

            if (dbArtist == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{artist.Name} could not be updated");
            }

            return StatusCode(StatusCodes.Status200OK, artist);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            var artist = await _libraryService.GetArtistAsync(id, false);
            (bool status, string message) = await _libraryService.DeleteArtistAsync(artist);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, artist);
        }
    }
}
