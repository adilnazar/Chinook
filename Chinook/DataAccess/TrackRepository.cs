using Chinook.Common.Constants;
using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Infrastructure.Contracts.Services;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Chinook.DataAccess
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinookContext DbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory"></param>
        public TrackRepository(IDbContextFactory<ChinookContext> dbFactory) 
        { 
            DbContext = dbFactory.CreateDbContext();
        }

        /// <summary>
        /// Retrieves a list of tracks for a given artist, including information on 
        /// whether each track is marked as a favorite by the specified user.
        /// </summary>
        /// <param name="artistId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ClientModels.PlaylistTrack>> GetTracksByArtist(long artistId,string userId)
        {
            var result = await DbContext.Tracks.Where(a => a.Album.ArtistId == artistId)
            .Include(a => a.Album)
            .Select(t => new ClientModels.PlaylistTrack()
            {
                AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                TrackId = t.TrackId,
                TrackName = t.Name,
                IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId && up.Playlist.Name == Constant.Favourite)).Any()
            })
            .OrderBy(t => t.AlbumTitle)
            .ThenBy(t => t.TrackName)
            .ToListAsync();

            return result;
        }

        /// <summary>
        /// Retrieves the favorite playlist for a given user.
        /// If no such playlist exists, it creates a new favorite playlist for the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<Playlist> GetOrCreateFavoritePlaylist(string userId)
        {
            var favoritePlaylist = await DbContext.Playlists
                .Include(p => p.UserPlaylists)
                .Include(x => x.Tracks)
                .FirstOrDefaultAsync(p => p.UserPlaylists.Any(up => up.UserId == userId) && p.Name == Constant.Favourite);

            if (favoritePlaylist == null)
            {
                var newPlaylistId = DbContext.Playlists.Max(p => p.PlaylistId) + 1;

                favoritePlaylist = new Playlist
                {
                    PlaylistId = newPlaylistId,
                    Name = Constant.Favourite,
                    UserPlaylists = new List<UserPlaylist>
                    {
                        new UserPlaylist { UserId = userId ,PlaylistId = newPlaylistId}
                    }
                };
                var result = DbContext.Playlists.Add(favoritePlaylist);
                await DbContext.SaveChangesAsync();
            }

            return favoritePlaylist;
        }

        /// <summary>
        /// Adds a track to the user's favorite playlist if it is not already present.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> AddTrackToFavorite(long trackId, string userId)
        {
            var favoritePlaylist = await GetOrCreateFavoritePlaylist(userId);

            if(!favoritePlaylist.Tracks.Any(x=>x.TrackId == trackId))
            {
                var dbTrack = await DbContext.Tracks.FindAsync(trackId);
                if (dbTrack != null)
                {
                    favoritePlaylist.Tracks.Add(dbTrack);
                    await DbContext.SaveChangesAsync();
                    return true;
                }
            }           
            
            return false;
        }

        /// <summary>
        /// Removes a track from the user's favorite playlist if it is present.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTrackFromFavorite(long trackId, string userId)
        {
            var favoritePlaylist = await GetOrCreateFavoritePlaylist(userId);

            if (favoritePlaylist.Tracks.Any(x => x.TrackId == trackId))
            {
                var dbTrack = await DbContext.Tracks.FindAsync(trackId);
                if (dbTrack != null)
                {
                    favoritePlaylist.Tracks.Remove(dbTrack);
                    await DbContext.SaveChangesAsync();
                    return true;
                }
            }               

            return false;
        }

        /// <summary>
        /// Adds a track to an existing playlist or creates a new playlist and adds the track to it.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <param name="newPlaylistName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> AddTrackToPlaylist(long trackId,long? playlistId, string? newPlaylistName, string userId)
        {
            try
            {
                var dbTrack = await DbContext.Tracks.FindAsync(trackId);
                if (dbTrack != null)
                {
                    //Add the track to relevant playlist
                    if (playlistId != null)
                    {
                        var playlist = await DbContext.Playlists
                                                      .Include(x => x.Tracks)
                                                      .FirstOrDefaultAsync(x => x.PlaylistId == playlistId);
                        if (playlist != null)
                        {
                            if(!playlist.Tracks.Any(x => x.TrackId == trackId))
                            {
                                playlist.Tracks.Add(dbTrack);
                            }
                            else
                            {
                                return "Selected Track Already Exist Selected Playlist";
                            }
                            
                        }
                    }
                    //Create a new playlist with the name provided if not exist and add the track
                    else
                    {
                        var playlistExist = await DbContext.Playlists
                                                        .Include(p => p.UserPlaylists)
                                                        .AnyAsync(p => p.UserPlaylists.Any(up => up.UserId == userId) 
                                                                       && p.Name != null 
                                                                       && newPlaylistName != null 
                                                                       && p.Name.ToLower() == newPlaylistName.ToLower());

                        if (playlistExist)
                        {
                            return $"Playlist Name '{newPlaylistName}' Already Exist.";
                        }
                        else
                        {
                            var newPlaylistId = DbContext.Playlists.Max(p => p.PlaylistId) + 1;

                            var newPlaylist = new Playlist
                            {
                                PlaylistId = newPlaylistId,
                                Name = newPlaylistName,
                                UserPlaylists = new List<UserPlaylist>
                            {
                                new UserPlaylist { UserId = userId ,PlaylistId = newPlaylistId}
                            }
                            };
                            var result = DbContext.Playlists.Add(newPlaylist);

                            result.Entity.Tracks.Add(dbTrack);
                        }

                    }
                    await DbContext.SaveChangesAsync();
                }

                return "";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Removes a track from a specified playlist.
        /// </summary>
        /// <param name="trackId"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveTrackFromPlaylist(long trackId, long playlistId)
        {
            var dbTrack = await DbContext.Tracks.FindAsync(trackId);
            var playlist = DbContext.Playlists.Include(x=>x.Tracks).FirstOrDefault(x=>x.PlaylistId == playlistId);
            if(playlist != null && dbTrack != null)
            {
                playlist.Tracks.Remove(dbTrack);
                DbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
