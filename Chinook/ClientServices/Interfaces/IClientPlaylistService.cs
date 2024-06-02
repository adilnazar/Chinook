using Chinook.ClientModels;

namespace Chinook.ClientServices.Interfaces
{
    public interface IClientPlaylistService
    {
        Task<List<Playlist>> GetPlaylistsAsync();
        Task<Playlist> GetPlaylistByIdAsync(long playlistId);
    }
}
