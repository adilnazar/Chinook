using Chinook.ClientModels;
using Chinook.ClientModels.Request;
using Chinook.ClientServices.API;
using Chinook.ClientServices.Interfaces;
using Chinook.DataAccess;
using Chinook.Infrastructure.Contracts.Services;
using Chinook.Models;
using Chinook.Services;

namespace Chinook.ClientServices
{
    public class ClientTrackService : IClientTrackService
    {
        private readonly IApiServices ApiServices;
        private readonly IUserService UserService;

        public ClientTrackService(IApiServices apiServices, IUserService userService)
        {
            ApiServices = apiServices;
            UserService = userService;

        }


        /// <summary>
        /// Retrieves a list of tracks for a given artist.
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        public async Task<List<PlaylistTrack>> GetTracksByArtist(long artistId)
        {
            var userId = await UserService.GetUserId();
            var requestData = new CommonRequestModel
            {
                Id = artistId,
                UserId = userId
            };
            return await ApiServices.PostDataAsync<CommonRequestModel, List<PlaylistTrack>>("api/track/getTracksByArtist", requestData);
        }

        /// <summary>
        /// Adds a track to the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public async Task<bool> FavoriteTrack(long trackId)
        {
            var userId = await UserService.GetUserId();
            var requestData = new CommonRequestModel
            {
                Id = trackId,
                UserId = userId
            };
            return await ApiServices.PostDataAsync<CommonRequestModel, bool>("api/track/favoriteTrack", requestData);
        }

        /// <summary>
        /// Removes a track from the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public async Task<bool> UnFavoriteTrack(long trackId)
        {
            var userId = await UserService.GetUserId();
            var requestData = new CommonRequestModel
            {
                Id = trackId,
                UserId = userId
            };
            return await ApiServices.PostDataAsync<CommonRequestModel, bool>("api/track/unFavoriteTrack", requestData);
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
            var requestData = new AddTrackToPlaylistModel
            {
                TrackId = trackId,
                PlaylistId = playlistId,
                NewPlaylistName = newPlaylistName,
                UserId = userId
            };
            return await ApiServices.PostDataAsync<AddTrackToPlaylistModel, string>("api/track/addTrackToPlaylist", requestData);
        }

        /// <summary>
        /// Removes a track from a specified playlist.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTrackFromPlaylist(long trackId, long playlistId)
        {
            var requestData = new RemoveTrackFromPlaylistModel
            {
                TrackId = trackId,
                PlaylistId = playlistId,
            };
            return await ApiServices.PostDataAsync<RemoveTrackFromPlaylistModel, bool>("api/track/removeTrackFromPlaylist", requestData);
        }
    }
}
