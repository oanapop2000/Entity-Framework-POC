using EntityFrameworkPOC.Data;
using EntityFrameworkPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPOC.Services
{
    public class ArtistLibraryService : IArtistLibraryService
    {
        private readonly AppDbContext _db;

        public ArtistLibraryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Artist>> GetArtistsAsync()
        {
            try
            {
                return await _db.Artists.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Artist> GetArtistAsync(Guid id, bool includeSongs)
        {
            try
            {
                if (includeSongs)
                {
                    return await _db.Artists.Include(s => s.Songs)
                        .FirstOrDefaultAsync(i => i.Id == id);
                }

                return await _db.Artists.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Artist> AddArtistAsync(Artist artist)
        {
            try
            {
                await _db.Artists.AddAsync(artist);
                await _db.SaveChangesAsync();
                return await _db.Artists.FindAsync(artist.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Artist> UpdateArtistAsync(Artist artist)
        {
            try
            {
                _db.Entry(artist).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return artist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteArtistAsync(Artist artist)
        {
            try
            {
                var dbAuthor = await _db.Artists.FindAsync(artist.Id);

                if (dbAuthor == null)
                {
                    return (false, "Artist could not be found");
                }

                _db.Artists.Remove(artist);
                await _db.SaveChangesAsync();

                return (true, "Artist got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
    }
}