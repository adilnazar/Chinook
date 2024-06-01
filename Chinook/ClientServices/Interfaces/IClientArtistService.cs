using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.ClientServices.Interfaces
{
    public interface IClientArtistService
    {
        Task<List<ArtistModel>> GetArtistsAsync(string search);
    }
}
