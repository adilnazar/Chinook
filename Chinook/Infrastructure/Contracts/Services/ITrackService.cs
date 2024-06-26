﻿using Chinook.ClientModels.Request;
using Chinook.Models;

namespace Chinook.Infrastructure.Contracts.Services
{
    public interface ITrackService : IBase
    {
        /// <summary>
        /// Retrieves a list of tracks for a given artist.
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        Task<List<ClientModels.PlaylistTrack>> GetTracksByArtist(CommonRequestModel requestModel);

        /// <summary>
        /// dds a track to the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        Task<bool> FavoriteTrack(CommonRequestModel requestModel);

        /// <summary>
        /// Removes a track from the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        Task<bool> UnFavoriteTrack(CommonRequestModel requestModel);

        /// <summary>
        /// Adds a track to an existing playlist or creates a new playlist and adds the track to it.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <param name="newPlaylistName"></param>
        /// <returns></returns>
        Task<string> AddTrackToPlaylist(AddTrackToPlaylistModel requestModel);

        /// <summary>
        /// Removes a track from a specified playlist.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        Task<bool> RemoveTrackFromPlaylist(RemoveTrackFromPlaylistModel requestModel);

    }
}
