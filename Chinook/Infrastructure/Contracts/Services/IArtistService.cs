using Chinook.Models;
using NuGet.Protocol.Core.Types;

namespace Chinook.Infrastructure.Contracts.Services
{
    public interface IArtistService : IBase
    {
        /// <summary>
        /// Get All Artist Avaiable for search string provided if not retrive all
        /// </summary>
        /// <param name="artistSearchString"></param>
        /// <returns></returns>
        Task<List<Artist>> GetArtistsAsync(string artistSearchString);
        /// <summary>
        /// Get Artist Details by Id
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        Task<Artist> GetArtistByIdAsync(long ArtistId);
    }
}
