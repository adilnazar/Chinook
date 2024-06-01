using Chinook.Models;

namespace Chinook.Infrastructure.Contracts.Repositories
{
    public interface ITrackRepository : IBase
    {
        /// <summary>
        /// Retrieves a list of tracks for a given artist, including information on 
        /// whether each track is marked as a favorite by the specified user.
        /// </summary>
        /// <param name="artistId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ClientModels.PlaylistTrack>> GetTracksByArtist(long artistId, string userId);

        /// <summary>
        /// Adds a track to the user's favorite playlist if it is not already present.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> AddTrackToFavorite(long trackId, string userId);

        /// <summary>
        /// Removes a track from the user's favorite playlist if it is present.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> RemoveTrackFromFavorite(long trackId, string userId);

        /// <summary>
        /// Adds a track to an existing playlist or creates a new playlist and adds the track to it.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <param name="newPlaylistName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> AddTrackToPlaylist(long trackId, long? playlistId, string? newPlaylistName, string userId);

        /// <summary>
        /// Removes a track from a specified playlist.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        Task<bool> RemoveTrackFromPlaylist(long trackId, long playlistId);
    }
}
