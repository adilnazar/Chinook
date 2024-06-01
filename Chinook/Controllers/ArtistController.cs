using Chinook.ClientModels;
using Chinook.Infrastructure.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ArtistController : ControllerBase
{
    private readonly IArtistService ArtistService;
    private readonly ILogger<ArtistController> Logger;

    public ArtistController(IArtistService artistService, ILogger<ArtistController> logger)
    {
        ArtistService = artistService;
        Logger = logger;
    }

    /// <summary>
    /// Get All Atists detail with albm count
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<ArtistModel>>> GetArtists([FromQuery] string search = "")
    {
        try
        {
            var artists = await ArtistService.GetArtistsAsync(search);
            return Ok(artists);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An unexpected error occurred while getting artists.");
            return StatusCode(500, new { message = "An error occurred while processing your request.", detail = ex.Message });
        }
    }
}