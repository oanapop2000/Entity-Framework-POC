using EntityFrameworkPOC.Models;
using EntityFrameworkPOC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkPOC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongLibraryService _libraryService;

        public SongController(ISongLibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSongs()
        {
            var songs = await _libraryService.GetSongsAsync();
            if (songs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No songs in database.");
            }

            return StatusCode(StatusCodes.Status200OK, songs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSong(Guid id)
        {
            Song song = await _libraryService.GetSongAsync(id);

            if (song == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No song found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, song);
        }

        [HttpPost]
        public async Task<ActionResult<Song>> AddSong(Song song)
        {
            var dbSong = await _libraryService.AddSongAsync(song);

            if (dbSong == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{song.Title} could not be added.");
            }

            return CreatedAtAction("GetSong", new { id = song.Id }, song);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(Guid id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            Song dbSong = await _libraryService.UpdateSongAsync(song);

            if (dbSong == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{song.Title} could not be updated");
            }

            return StatusCode(StatusCodes.Status200OK, song);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            var song = await _libraryService.GetSongAsync(id);
            (bool status, string message) = await _libraryService.DeleteSongAsync(song);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, song);
        }
    }
}
