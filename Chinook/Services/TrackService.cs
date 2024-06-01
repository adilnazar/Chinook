using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Infrastructure.Contracts.Services;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Chinook.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository TrackRepository;
        private readonly IUserService UserService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userService"></param>
        public TrackService(ITrackRepository repository,IUserService userService) 
        {
            TrackRepository = repository;
            UserService = userService;
        }

        /// <summary>
        /// Retrieves a list of tracks for a given artist.
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        public async Task<List<ClientModels.PlaylistTrack>> GetTracksByArtist(long artistId)
        {
            var userId = await UserService.GetUserId();
            var result = await TrackRepository.GetTracksByArtist(artistId,userId);
            return result;
        }

        /// <summary>
        /// Adds a track to the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public async Task<bool> FavoriteTrack(long trackId)
        {
            var userId = await UserService.GetUserId();
            var result = await TrackRepository.AddTrackToFavorite(trackId, userId);
            return result;
        }

        /// <summary>
        /// Removes a track from the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public async Task<bool> UnFavoriteTrack(long trackId)
        {
            var userId = await UserService.GetUserId();
            var result = await TrackRepository.RemoveTrackFromFavorite(trackId, userId);
            return result;
        }

        /// <summary>
        /// Adds a track to an existing playlist or creates a new playlist and adds the track to it.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <param name="newPlaylistName"></param>
        /// <returns></returns>
        public async Task<string> AddTrackToPlaylist(long trackId, long? playlistId, string? newPlaylistName)
        {
            var userId = await UserService.GetUserId();
            var result = await TrackRepository.AddTrackToPlaylist(trackId, playlistId, newPlaylistName,userId);

            return result;
        }

        /// <summary>
        /// Removes a track from a specified playlist.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTrackFromPlaylist(long trackId, long playlistId)
        {
            var result = await TrackRepository.RemoveTrackFromPlaylist(trackId, playlistId);
            return result;
        }
    }
}
