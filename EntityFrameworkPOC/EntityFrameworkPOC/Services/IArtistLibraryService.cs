using EntityFrameworkPOC.Models;

namespace EntityFrameworkPOC.Services
{
    public interface IArtistLibraryService
    {
        Task<List<Artist>> GetArtistsAsync(); 
        Task<Artist> GetArtistAsync(Guid id, bool includeSongs = false); 
        Task<Artist> AddArtistAsync(Artist artist); 
        Task<Artist> UpdateArtistAsync(Artist artist); 
        Task<(bool, string)> DeleteArtistAsync(Artist artist); 
    }
}
