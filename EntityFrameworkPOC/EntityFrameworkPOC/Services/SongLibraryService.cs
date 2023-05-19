using EntityFrameworkPOC.Data;
using EntityFrameworkPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPOC.Services
{
    public class SongLibraryService : ISongLibraryService
    {
        private readonly AppDbContext _db;

        public SongLibraryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Song>> GetSongsAsync()
        {
            try
            {
                return await _db.Songs.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Song> GetSongAsync(Guid id)
        {
            try
            {
                return await _db.Songs.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Song> AddSongAsync(Song song)
        {
            try
            {
                await _db.Songs.AddAsync(song);
                await _db.SaveChangesAsync();
                return await _db.Songs.FindAsync(song.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Song> UpdateSongAsync(Song song)
        {
            try
            {
                _db.Entry(song).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return song;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSongAsync(Song song)
        {
            try
            {
                var dbSong = await _db.Songs.FindAsync(song.Id);

                if (dbSong== null)
                {
                    return (false, "Song could not be found.");
                }

                _db.Songs.Remove(song);
                await _db.SaveChangesAsync();

                return (true, "Song got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
    }
}
