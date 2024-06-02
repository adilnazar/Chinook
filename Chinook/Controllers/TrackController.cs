using Chinook.ClientModels.Request;
using Chinook.Infrastructure.Contracts.Services;
using Chinook.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chinook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService TrackService;
        private readonly ILogger<TrackController> Logger;

        public TrackController(ITrackService trackService, ILogger<TrackController> logger)
        {
            TrackService = trackService;
            Logger = logger;
        }

        [HttpPost("getTracksByArtist")]
        public async Task<ActionResult<List<ClientModels.PlaylistTrack>>> GetTracksByArtist([FromBody] CommonRequestModel requestModel)
        {
            try
            {
                var tracks = await TrackService.GetTracksByArtist(requestModel);
                return tracks;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred while getting tracks by artist.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("favoriteTrack")]
        public async Task<ActionResult<bool>> FavoriteTrack([FromBody] CommonRequestModel requestModel)
        {
            try
            {
                var result = await TrackService.FavoriteTrack(requestModel);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred while add track to favorite.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("unFavoriteTrack")]
        public async Task<ActionResult<bool>> UnFavoriteTrack([FromBody] CommonRequestModel requestModel)
        {
            try
            {
                var result = await TrackService.UnFavoriteTrack(requestModel);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred while remove track from favorite.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("addTrackToPlaylist")]
        public async Task<ActionResult<string>> AddTrackToPlaylist([FromBody] AddTrackToPlaylistModel requestModel)
        {
            try
            {
                var result = await TrackService.AddTrackToPlaylist(requestModel);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred while add track to playlist.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("removeTrackFromPlaylist")]
        public async Task<ActionResult<bool>> RemoveTrackFromPlaylist([FromBody] RemoveTrackFromPlaylistModel requestModel)
        {
            try
            {
                var result = await TrackService.RemoveTrackFromPlaylist(requestModel);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An unexpected error occurred while remove track from playlist.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
