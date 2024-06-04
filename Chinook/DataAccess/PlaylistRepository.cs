using Microsoft.EntityFrameworkCore;

using Chinook.Common.Constants;
using Chinook.Infrastructure.Contracts.Repositories;


namespace Chinook.DataAccess
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ChinookContext DbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory"></param>
        public PlaylistRepository(IDbContextFactory<ChinookContext> dbFactory)
        {
            DbContext = dbFactory.CreateDbContext();
        }

        /// <summary>
        /// Retrieves a list of playlists for a given user, excluding the favorite playlist.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ClientModels.Playlist>> GetUserPlayLists(string userId)
        {
            try
            {
                var playlists = await DbContext.UserPlaylists
                                               .Include(x => x.Playlist)
                                               .Where(x => x.UserId == userId)
                                               .Select(x => new ClientModels.Playlist
                                               {
                                                   PlaylistId = x.Playlist.PlaylistId,
                                                   Name = x.Playlist.Name
                                               }).OrderBy(x => x.Name != Constant.Favourite).ThenBy(x=>x.Name).ToListAsync();

                return playlists;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retrieves a playlist by Id along with its associated tracks, 
        /// including album and artist details. Additionally, it checks if the tracks 
        /// are marked as favorite for a given user.
        /// </summary>
        /// <param name="playlistId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ClientModels.Playlist> GetPlayListByIdWithTracks(long playlistId, string userId)
        {
            try
            {
                var playList = await DbContext.Playlists
                                              .Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist)
                                              .Where(p => p.PlaylistId == playlistId)
                                              .Select(p => new ClientModels.Playlist()
                                              {
                                                  Name = p.Name,
                                                  Tracks = p.Tracks.Select(t => new ClientModels.PlaylistTrack()
                                                  {
                                                      AlbumTitle = t.Album.Title,
                                                      ArtistName = t.Album.Artist.Name,
                                                      TrackId = t.TrackId,
                                                      TrackName = t.Name,
                                                      IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId 
                                                                                     && up.Playlist.Name == Constant.Favourite)).Any()
                                                  }).OrderBy(x => x.ArtistName).ThenBy(x => x.AlbumTitle).ThenBy(x => x.TrackName).ToList()
                                              })
                                              .FirstOrDefaultAsync();

                return playList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
