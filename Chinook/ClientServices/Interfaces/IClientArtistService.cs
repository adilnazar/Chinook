using Chinook.ClientModels;
using Chinook.Models;

namespace Chinook.ClientServices.Interfaces
{
    public interface IClientArtistService
    {
        /// <summary>
        /// Get Artists list From Server 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<List<ArtistModel>> GetArtistsAsync(string search);

        /// <summary>
        /// Get Artist detail by Id from Server
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        Task<ArtistModel> GetArtistByIdAsync(long artistId);
    }
}
