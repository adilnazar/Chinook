using Chinook.ClientModels;

namespace Chinook.ClientServices.Interfaces
{
    public interface IClientTrackService
    {
        /// <summary>
        /// Retrieves a list of tracks for a given artist.
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        Task<List<PlaylistTrack>> GetTracksByArtist(long artistId);

        /// <summary>
        /// dds a track to the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        Task<bool> FavoriteTrack(long trackId);

        /// <summary>
        /// Removes a track from the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        Task<bool> UnFavoriteTrack(long trackId);

        /// <summary>
        /// Adds a track to an existing playlist or creates a new playlist and adds the track to it.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <param name="newPlaylistName"></param>
        /// <returns></returns>
        Task<string> AddTrackToPlaylist(long trackId, long? playlistId, string? newPlaylistName);

        /// <summary>
        /// Removes a track from a specified playlist.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        Task<bool> RemoveTrackFromPlaylist(long trackId, long playlistId);
    }
}
