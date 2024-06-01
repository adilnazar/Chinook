using Chinook.Models;

namespace Chinook.Infrastructure.Contracts.Repositories
{
    public interface IPlaylistRepository : IBase
    {
        /// <summary>
        /// Retrieves a list of playlists for a given user, excluding the favorite playlist.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ClientModels.Playlist>> GetUserPlayLists(string userId);

        /// <summary>
        /// Retrieves a playlist by Id along with its associated tracks, 
        /// including album and artist details. Additionally, it checks if the tracks 
        /// are marked as favorite for a given user.
        /// </summary>
        /// <param name="playlistId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ClientModels.Playlist> GetPlayListByIdWithTracks(long playlistId, string userId);
    }
}
