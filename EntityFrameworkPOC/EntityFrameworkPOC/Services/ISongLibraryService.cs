using EntityFrameworkPOC.Models;

namespace EntityFrameworkPOC.Services
{
    public interface ISongLibraryService
    {
        Task<List<Song>> GetSongsAsync();
        Task<Song> GetSongAsync(Guid id);
        Task<Song> AddSongAsync(Song song);
        Task<Song> UpdateSongAsync(Song song);
        Task<(bool, string)> DeleteSongAsync(Song song);
    }
}
