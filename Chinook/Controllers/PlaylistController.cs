using Chinook.ClientModels;
using Chinook.Infrastructure.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chinook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService PlaylistService;
        private readonly ILogger<PlaylistController> Logger;

        public PlaylistController(IPlaylistService playlistService, ILogger<PlaylistController> logger)
        {
            PlaylistService = playlistService;
            Logger = logger;
        }

        /// <summary>
        /// Get All Playlists for the user
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Playlist>>> GetPlaylists(string userId)
        {
            try
            {
                var playlists = await PlaylistService.GetUserPlayLists(userId);
                return Ok(playlists);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred while getting playlists.");
                return StatusCode(500, new { message = "An error occurred while processing your request.", detail = ex.Message });
            }
        }

        /// <summary>
        /// Get Playlist detail by Id
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        [HttpGet("GetPlaylistById/{userId}/{playlistId}")]
        public async Task<ActionResult<Playlist>> GetPlaylistById(string userId, long playlistId)
        {
            try
            {
                var playlist = await PlaylistService.GetPlayListByIdWithTracks(userId, playlistId);
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred while getting playlist by Id.");
                return StatusCode(500, new { message = "An error occurred while processing your request.", detail = ex.Message });
            }
        }
    }
}
