using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Infrastructure.Contracts.Services;

namespace Chinook.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository PlaylistRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public PlaylistService(IPlaylistRepository repository)
        {
            PlaylistRepository = repository;
        }

        /// <summary>
        /// Retrieves a list of playlists for a given user.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ClientModels.Playlist>> GetUserPlayLists(string userId)
        {
            try
            {
                var playlists = await PlaylistRepository.GetUserPlayLists(userId);

                return playlists;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Retrieves a playlist by Id along with its associated tracks.
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public async Task<ClientModels.Playlist> GetPlayListByIdWithTracks(string userId,long playlistId)
        {
            try
            {
                var playlistWithTracks = await PlaylistRepository.GetPlayListByIdWithTracks(playlistId, userId);

                return playlistWithTracks;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
