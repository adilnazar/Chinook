using Chinook.DataAccess;
using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Infrastructure.Contracts.Services;
using Chinook.Models;
using NuGet.Protocol.Core.Types;

namespace Chinook.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository PlaylistRepository;
        private readonly IUserService UserService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public PlaylistService(IPlaylistRepository repository, IUserService userService)
        {
            PlaylistRepository = repository;
            UserService = userService;
        }

        /// <summary>
        /// Retrieves a list of playlists for a given user.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClientModels.Playlist>> GetUserPlayLists()
        {
            var userId = await UserService.GetUserId();
            var playlists = await PlaylistRepository.GetUserPlayLists(userId);

            return playlists;

        }

        /// <summary>
        /// Retrieves a playlist by Id along with its associated tracks.
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public async Task<ClientModels.Playlist> GetPlayListByIdWithTracks(long playlistId)
        {
            var userId = await UserService.GetUserId();
            var playlistWithTracks = await PlaylistRepository.GetPlayListByIdWithTracks(playlistId, userId);

            return playlistWithTracks;
        }


    }
}
