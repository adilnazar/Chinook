using Chinook.DataAccess;
using Chinook.Models;
using Chinook.Services;

namespace Chinook.Infrastructure.Contracts.Services
{
    public interface IPlaylistService : IBase
    {
        /// <summary>
        /// Retrieves a list of playlists for a given user.
        /// </summary>
        /// <returns></returns>
        Task<List<ClientModels.Playlist>> GetUserPlayLists(string userId);

        /// <summary>
        /// Retrieves a playlist by Id along with its associated tracks.
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        Task<ClientModels.Playlist> GetPlayListByIdWithTracks(string userId,long playlistId);
    }
}
