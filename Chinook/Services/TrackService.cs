using Chinook.ClientModels.Request;
using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Infrastructure.Contracts.Services;

namespace Chinook.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository TrackRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userService"></param>
        public TrackService(ITrackRepository repository) 
        {
            TrackRepository = repository;
        }

        /// <summary>
        /// Retrieves a list of tracks for a given artist.
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        public async Task<List<ClientModels.PlaylistTrack>> GetTracksByArtist(CommonRequestModel requestModel)
        {
            var result = await TrackRepository.GetTracksByArtist(requestModel.Id, requestModel.UserId);
            return result;
        }

        /// <summary>
        /// Adds a track to the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public async Task<bool> FavoriteTrack(CommonRequestModel requestModel)
        {
            var result = await TrackRepository.AddTrackToFavorite(requestModel.Id, requestModel.UserId);
            return result;
        }

        /// <summary>
        /// Removes a track from the user's favorite playlist
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        public async Task<bool> UnFavoriteTrack(CommonRequestModel requestModel)
        {
            var result = await TrackRepository.RemoveTrackFromFavorite(requestModel.Id, requestModel.UserId);
            return result;
        }

        /// <summary>
        /// Adds a track to an existing playlist or creates a new playlist and adds the track to it.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <param name="newPlaylistName"></param>
        /// <returns></returns>
        public async Task<string> AddTrackToPlaylist(AddTrackToPlaylistModel requestModel)
        {
            var result = await TrackRepository.AddTrackToPlaylist(requestModel.TrackId, requestModel.PlaylistId, requestModel.NewPlaylistName, requestModel.UserId);

            return result;
        }

        /// <summary>
        /// Removes a track from a specified playlist.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTrackFromPlaylist(RemoveTrackFromPlaylistModel requestModel)
        {
            var result = await TrackRepository.RemoveTrackFromPlaylist(requestModel.TrackId, requestModel.PlaylistId);
            return result;
        }
    }
}
